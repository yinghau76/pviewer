using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Reflection;

namespace PViewer
{
	/// <summary>
	/// Summary description for Utility.
	/// </summary>
	public sealed class Utility
	{
		public Utility()
		{
			//
			// TODO: Add constructor logic here
			//
		}

#if DEBUG

        public static string Indent(int indentLevel)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 4 * indentLevel; ++i)
            {
                sb.Append(" ");
            }

            return sb.ToString();
        }

        public static void WalkGraph(object walkMe)
        {
            Hashtable walkedObjects = new Hashtable();
            StreamWriter output = new StreamWriter(@"c:\walk.txt");
            output.WriteLine("GC.GetTotalMemory returned " + GC.GetTotalMemory(true).ToString());
            output.WriteLine("Starting walk:");
            
            WalkGraph(output, walkMe, walkedObjects, 0);

            output.Close();
        }

        public static void AddToSet(Hashtable objects, object o)
        {
            Type type = o.GetType();
            Set set = (Set)objects[type];

            if (set == null)
            {
                set = new Set();
                objects[type] = set;
            }

            if (set.Contains(o))
            {
                throw new ArgumentException();
            }

            set.Add(o);
        }

        private unsafe static void WalkGraph(TextWriter output, object walkMe, Hashtable walkedObjects, int indentLevel)
        {
            if (walkMe == null)
            {
                output.WriteLine(Indent(indentLevel) + "(null)");
                return;
            }

            Type walkMeType = walkMe.GetType();
            MemberInfo[] fields = walkMeType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            MemberInfo[] properties = walkMeType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            ArrayList members = new ArrayList();

            if (fields.Length > 0)
            {
                output.WriteLine(Indent(indentLevel) + " fields:");
            }

            foreach (FieldInfo fi in fields)
            {
                object fieldValue = fi.GetValue(walkMe);
                bool traversed = false;

                try
                {
                    if (fieldValue != null)
                    {
                        AddToSet(walkedObjects, fieldValue);
                    }
                }

                catch (ArgumentException)
                {
                    traversed = true;
                }

                output.WriteLine(Indent(indentLevel + 1)  + fi.FieldType.ToString() + " " + fi.Name + " (hash=" + (fieldValue == null ? "n/a" : fieldValue.GetHashCode().ToString()) + ")" + (traversed ? (fieldValue == null ? "" : " (already traversed)") : ":"));

                if (!traversed && fi.FieldType != typeof(void*) && fieldValue != null)
                {
                    WalkGraph(output, fieldValue, walkedObjects, indentLevel + 1);
                }
            }

            if (properties.Length > 0)
            {
                output.WriteLine(Indent(indentLevel) + " properties:");
            }

            foreach (PropertyInfo pi in properties)
            {
                if (pi.GetIndexParameters().Length > 0)
                {
                    continue;
                }

                object fieldValue = null;
                try
                {
                    fieldValue = pi.GetValue(walkMe, new object[0] {} );
                }

                catch
                {
                    output.WriteLine(Indent(indentLevel) + "exception calling PropertyInfo.GetValue(" + pi.Name + ")");
                }


                bool traversed = false;

                try
                {
                    if (fieldValue != null)
                    {
                        AddToSet(walkedObjects, fieldValue);
                    }
                }

                catch (ArgumentException)
                {
                    traversed = true;
                }

                output.WriteLine(Indent(indentLevel + 1)  + pi.PropertyType.ToString() + " " + pi.Name + " (hash=" + (fieldValue == null ? "n/a" : fieldValue.GetHashCode().ToString()) + ")" + (traversed ? (fieldValue == null ? "" : " (already traversed)") : ":"));

                if (!traversed && pi.PropertyType != typeof(void*) && fieldValue != null)
                {
                    WalkGraph(output, fieldValue, walkedObjects, indentLevel + 1);
                }
            }

            if (walkMe is IEnumerable && !(walkMe is string))
            {
                int count = 0;

                output.WriteLine(Indent(indentLevel) + " enumerated items via IEnumerable:");

                foreach (object o in (IEnumerable)walkMe)
                {
                    if (count > 10)
                    {
                        break;
                    }

                    ++count;

                    if (o != null)
                    {
                        bool traversed = false;

                        try
                        {
                            AddToSet(walkedObjects, o);
                        }

                        catch (ArgumentException)
                        {
                            traversed = true;
                        }

                        output.WriteLine(Indent(indentLevel + 1)  + count.ToString() + ": typeof(" + o.GetType().ToString() + ") (hash=" + (o == null ? "n/a" : o.GetHashCode().ToString()) + ")" + (traversed ? (o == null ? "" : " (already traversed)") : ":"));
                        WalkGraph(output, o, walkedObjects, indentLevel + 1);
                    }
                }
            }
        }
#endif
	}
}
