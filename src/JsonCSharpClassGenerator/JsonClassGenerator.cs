// Copyright © 2010 Xamasoft

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using Xamasoft.JsonClassGenerator.CodeWriters;
using PluralizeService.Core;

namespace Xamasoft.JsonClassGenerator
{
    public class JsonClassGenerator : IJsonClassGeneratorConfig
    {
        public string Example { get; set; }
        public string TargetFolder { get; set; }
        public string Namespace { get; set; }
        public string SecondaryNamespace { get; set; }
        public bool UseProperties { get; set; }
        public bool InternalVisibility { get; set; }
        public bool ExplicitDeserialization { get; set; }
        public bool NoHelperClass { get; set; }
        public string MainClass { get; set; }
        public bool SortMemberFields { get; set; }
        public bool UsePascalCase { get; set; }
        public bool UseNestedClasses { get; set; }
        public bool ApplyObfuscationAttributes { get; set; }
        public bool SingleFile { get; set; }
        public ICodeWriter CodeWriter { get; set; }
        public TextWriter OutputStream { get; set; }
        public bool AlwaysUseNullableValues { get; set; }
        public bool ExamplesInDocumentation { get; set; }
        public bool DeduplicateClasses { get; set; }

        public Action<string> FeedBack { get; set; }

        private bool used = false;
        public bool UseNamespaces { get { return Namespace != null; } }

        public void GenerateClasses()
        {
            if (CodeWriter == null) CodeWriter = new CSharpCodeWriter();
            if (ExplicitDeserialization && !(CodeWriter is CSharpCodeWriter)) throw new ArgumentException("Explicit deserialization is obsolete and is only supported by the C# provider.");

            if (used) throw new InvalidOperationException("This instance of JsonClassGenerator has already been used. Please create a new instance.");
            used = true;


            var writeToDisk = TargetFolder != null;
            if (writeToDisk && !Directory.Exists(TargetFolder)) Directory.CreateDirectory(TargetFolder);


            JObject[] examples;
            var example = Example.StartsWith("HTTP/") ? Example.Substring(Example.IndexOf("\r\n\r\n")) : Example;
            using (var sr = new StringReader(example))
            using (var reader = new JsonTextReader(sr))
            {
                var json = JToken.ReadFrom(reader);
                if (json is JArray)
                {
                    examples = ((JArray)json).Cast<JObject>().ToArray();
                }
                else if (json is JObject)
                {
                    examples = new[] { (JObject)json };
                }
                else
                {
                    throw new Exception("Sample JSON must be either a JSON array, or a JSON object.");
                }
            }

            Types = new List<JsonType>();
            Names.Add(MainClass);
            var rootType = new JsonType(this, examples[0]);
            rootType.IsRoot = true;
            rootType.AssignName(MainClass, MainClass);
            GenerateClass(examples, rootType);

            if (DeduplicateClasses)
            {
                FeedBack?.Invoke("De-duplicating classes");
                DeDuplicateClasses();
            }

            FeedBack?.Invoke("Writing classes to disk.");
            if (writeToDisk)
            {

                var parentFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                if (writeToDisk && !NoHelperClass && ExplicitDeserialization) File.WriteAllBytes(Path.Combine(TargetFolder, "JsonClassHelper.cs"), Properties.Resources.JsonClassHelper);
                if (SingleFile)
                {
                    WriteClassesToFile(Path.Combine(TargetFolder, MainClass + CodeWriter.FileExtension), Types);
                }
                else
                {

                    foreach (var type in Types)
                    {
                        var folder = TargetFolder;
                        if (!UseNestedClasses && !type.IsRoot && SecondaryNamespace != null)
                        {
                            var s = SecondaryNamespace;
                            if (s.StartsWith(Namespace + ".")) s = s.Substring(Namespace.Length + 1);
                            folder = Path.Combine(folder, s);
                            Directory.CreateDirectory(folder);
                        }
                        WriteClassesToFile(Path.Combine(folder, (UseNestedClasses && !type.IsRoot ? MainClass + "." : string.Empty) + type.AssignedName + CodeWriter.FileExtension), new[] { type });
                    }
                }
            }
            else if (OutputStream != null)
            {
                WriteClassesToFile(OutputStream, Types);
            }

        }

        /// <summary>
        /// De-duplicate classes.
        /// </summary>
        /// <remarks>
        /// So, we have a bunch of classes. Th eproblem is, if structures have been nested, we might end up with
        /// many classes which are all duplicates in everything but name. This bit of logic is intended to clean
        /// this up as best it can.
        /// 
        /// First, we get all of the "base" classes. These are all of the classes that were the first generated 
        /// classes. The first occurrence of any class. We alwys want these.
        /// 
        /// Next we may (or may not) have a list of classes that may (or may not) be duplicates of the first occurrence
        /// class. For example, assume we have a first occurrence class called "Wombat". Nested clases may have been
        /// generated called "Womnats2" or "Wombats3". All three classes may have the same content, so we need to 
        /// discard the copy classes and replace any references to them with the original Wonbats class.
        /// 
        /// This is fun.
        /// 
        /// </remarks>
        private void DeDuplicateClasses()
        {
            // Get the first occurrence classes (original name = assigned name) as we always want these
            var newTypes = (from tt in Types
                where string.Compare(tt.OriginalName, tt.AssignedName, StringComparison.InvariantCultureIgnoreCase) == 0
                select tt).ToList();

            // If we replace references to classes (Say "Wombats2" with "Womnbats", we need to know it has 
            // happen4ed and we need to fix the fields. This is the list of translations.
            var typeNameReplacements = new Dictionary<string, string>();

            // Get the potential duplicate classes. These are classes where the class name does not match
            // the original name (i.e. we added a count to it).
            var possibleDuplicates = from tt in Types
                where string.Compare(tt.OriginalName, tt.AssignedName, StringComparison.InvariantCultureIgnoreCase) != 0
                select tt;

            try
            {
                // Check the dupliates to see if they are the same as the first occurrence classes. Add to the first
                // occurrence list if  they are different or create field fixup's if they are the same. We are very
                // simplistic in testing for the "same" or "different". Do they hae the same number of fields and
                // are the field names the same. (Note, cannot use field types as these may be one of our classes that
                // we are foing to replace e.g. Wombats2 that will be replaced with Wombats).
                foreach (var duplicate in possibleDuplicates)
                {
                    var original = newTypes.FirstOrDefault(tt => tt.OriginalName == duplicate.OriginalName);
                    
                    if (FirstOccurrenceClassNotFound(original))
                    {
                        newTypes.Add(duplicate);
                        continue;
                    }

                    // Classes are the same - Merge the fields
                    MergeFieldFromDuplicateToOriginal(original, duplicate);

                    // Two objects are the 'same', so we want to replace the duplicate with the original. We will
                    // need to fix-up the field types when we are done.
                    typeNameReplacements.Add(duplicate.AssignedName, original.AssignedName);
                }

                // We now need to apply our class name translations to the new base types list. So, something that
                // might currently be referring to Wombats2 wil be changed to refer to Wombats.
                foreach (var jsonType in newTypes)
                {
                    foreach (var field in jsonType.Fields)
                    {
                        var internalTypeName = GetInternalTypeName(field);
                        if (internalTypeName != null && typeNameReplacements.ContainsKey(internalTypeName))
                        {
                            field.Type.InternalType.AssignName(typeNameReplacements[internalTypeName], typeNameReplacements[internalTypeName]);
                        }

                        var typeName = GetTypeName(field);
                        if (typeName != null && typeNameReplacements.ContainsKey(typeName))
                        {
                            field.Type.AssignName(typeNameReplacements[typeName], typeNameReplacements[typeName]);
                        }
                    }
                }

                // Replace the previous type list with the new type list
                Types.Clear();
                newTypes.ForEach(tt => Types.Add(tt));
            }
            catch(Exception ex)
            {
                // Worst case scenario - deduplication failed, so generate all the classes.
                Debug.Print($"Deduplication failed:\r\n\n{ex.Message}\r\n\r\n{ex.StackTrace}");
            }
        }

        private void MergeFieldFromDuplicateToOriginal(JsonType original, JsonType duplicate)
        {
            var fieldDifferences = GetFieldDifferences(original.Fields, duplicate.Fields, x => x.MemberName);
            foreach (var fieldDifference in fieldDifferences)
            {
                original.Fields.Add(duplicate.Fields.First(fld => fld.MemberName == fieldDifference));
            }
        }

        private string GetInternalTypeName(FieldInfo field)
        {
            // Sorry about this, but we can get nulls at all sorts of levels. Quite irritating really. So we have to
            // check all the way down to get the assigned name. Returns blank if we fail at any point.
            return field?.Type?.InternalType?.AssignedName;
        }

        private string GetTypeName(FieldInfo field)
        {
            // Sorry about this, but we can get nulls at all sorts of levels. Quite irritating really. So we have to
            // check all the way down to get the assigned name. Returns blank if we fail at any point.
            return field?.Type?.AssignedName;
        }

        private bool FirstOccurrenceClassNotFound(JsonType original) { return original == null; }

        public IEnumerable<string> GetFieldDifferences(IEnumerable<FieldInfo> source, IEnumerable<FieldInfo> other, Func<FieldInfo, string> keySelector)
        {
            var setSource = new HashSet<string>(source.Select(keySelector));
            var setOther = new HashSet<string>(other.Select(keySelector));

            return setOther.Except(setSource);
        }

        private void WriteClassesToFile(string path, IEnumerable<JsonType> types)
        {
            FeedBack?.Invoke($"Writing {path}");
            using (var sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                WriteClassesToFile(sw, types);
            }
        }

        private void WriteClassesToFile(TextWriter sw, IEnumerable<JsonType> types)
        {
            var inNamespace = false;
            var rootNamespace = false;

            CodeWriter.WriteFileStart(this, sw);
            foreach (var type in types)
            {
                if (UseNamespaces && inNamespace && rootNamespace != type.IsRoot && SecondaryNamespace != null) { CodeWriter.WriteNamespaceEnd(this, sw, rootNamespace); inNamespace = false; }
                if (UseNamespaces && !inNamespace) { CodeWriter.WriteNamespaceStart(this, sw, type.IsRoot); inNamespace = true; rootNamespace = type.IsRoot; }
                CodeWriter.WriteClass(this, sw, type);
            }
            if (UseNamespaces && inNamespace) CodeWriter.WriteNamespaceEnd(this, sw, rootNamespace);
            CodeWriter.WriteFileEnd(this, sw);
        }


        private void GenerateClass(JObject[] examples, JsonType type)
        {
            var jsonFields = new Dictionary<string, JsonType>();
            var fieldExamples = new Dictionary<string, IList<object>>();

            var first = true;

            foreach (var obj in examples)
            {
                foreach (var prop in obj.Properties())
                {
                    JsonType fieldType;
                    var currentType = new JsonType(this, prop.Value);
                    var propName = prop.Name;
                    if (jsonFields.TryGetValue(propName, out fieldType))
                    {

                        var commonType = fieldType.GetCommonType(currentType);

                        jsonFields[propName] = commonType;
                    }
                    else
                    {
                        var commonType = currentType;
                        if (first) commonType = commonType.MaybeMakeNullable(this);
                        else commonType = commonType.GetCommonType(JsonType.GetNull(this));
                        jsonFields.Add(propName, commonType);
                        fieldExamples[propName] = new List<object>();
                    }
                    var fe = fieldExamples[propName];
                    var val = prop.Value;
                    if (val.Type == JTokenType.Null || val.Type == JTokenType.Undefined)
                    {
                        if (!fe.Contains(null))
                        {
                            fe.Insert(0, null);
                        }
                    }
                    else
                    {
                        var v = val.Type == JTokenType.Array || val.Type == JTokenType.Object ? val : val.Value<object>();
                        if (!fe.Any(x => v.Equals(x)))
                            fe.Add(v);
                    }
                }
                first = false;
            }

            if (UseNestedClasses)
            {
                foreach (var field in jsonFields)
                {
                    Names.Add(field.Key.ToLower());
                }
            }

            foreach (var field in jsonFields)
            {
                var fieldType = field.Value;
                if (fieldType.Type == JsonTypeEnum.Object)
                {
                    var subexamples = new List<JObject>(examples.Length);
                    foreach (var obj in examples)
                    {
                        JToken value;
                        if (obj.TryGetValue(field.Key, out value))
                        {
                            if (value.Type == JTokenType.Object)
                            {
                                subexamples.Add((JObject)value);
                            }
                        }
                    }

                    fieldType.AssignName(CreateUniqueClassName(field.Key), field.Key);
                    GenerateClass(subexamples.ToArray(), fieldType);
                }

                if (fieldType.InternalType != null && fieldType.InternalType.Type == JsonTypeEnum.Object)
                {
                    var subexamples = new List<JObject>(examples.Length);
                    foreach (var obj in examples)
                    {
                        JToken value;
                        if (obj.TryGetValue(field.Key, out value))
                        {
                            if (value.Type == JTokenType.Array)
                            {
                                foreach (var item in (JArray)value)
                                {
                                    if (!(item is JObject)) throw new NotSupportedException("Arrays of non-objects are not supported yet.");
                                    subexamples.Add((JObject)item);
                                }

                            }
                            else if (value.Type == JTokenType.Object)
                            {
                                foreach (var item in (JObject)value)
                                {
                                    if (!(item.Value is JObject)) throw new NotSupportedException("Arrays of non-objects are not supported yet.");

                                    subexamples.Add((JObject)item.Value);
                                }
                            }
                        }
                    }

                    field.Value.InternalType.AssignName(CreateUniqueClassNameFromPlural(field.Key), 
                            ConvertPluralToSingle(field.Key));
                    GenerateClass(subexamples.ToArray(), field.Value.InternalType);
                }
            }

            type.Fields = jsonFields.Select(x => new FieldInfo(this, x.Key, x.Value, UsePascalCase, fieldExamples[x.Key])).ToList();

            FeedBack?.Invoke($"Generating class {type.AssignedName}");
            Types.Add(type);
        }

        public IList<JsonType> Types { get; private set; }
        private HashSet<string> Names = new HashSet<string>();

        private string CreateUniqueClassName(string name)
        {
            name = ToTitleCase(name);

            var finalName = name;
            var i = 2;
            while (Names.Any(x => x.Equals(finalName, StringComparison.OrdinalIgnoreCase)))
            {
                finalName = name + i.ToString();
                i++;
            }

            Names.Add(finalName);
            return finalName;
        }

        private string CreateUniqueClassNameFromPlural(string plural)
        {
            plural = ToTitleCase(plural);
            return CreateUniqueClassName(PluralizationProvider.Singularize(plural));
        }

        private string ConvertPluralToSingle(string plural)
        {
            plural = ToTitleCase(plural);
            return PluralizationProvider.Singularize(plural);
        }



        internal static string ToTitleCase(string str)
        {
            var sb = new StringBuilder(str.Length);
            var flag = true;

            for (int i = 0; i < str.Length; i++)
            {
                var c = str[i];
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(flag ? char.ToUpper(c) : c);
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }

            return sb.ToString();
        }

        public bool HasSecondaryClasses
        {
            get { return Types.Count > 1; }
        }

        public static readonly string[] FileHeader = new[] { 
            "Generated by Xamasoft JSON Class Generator", 
            "http://www.xamasoft.com/json-class-generator"
        };
    }
}
