using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Assets.Scripts.Poke;
using UnityEditor;

namespace Assets.Scripts.Extensions {
    public static class ExtensionsEnum {

        public static string ToString(this Moves moves, bool b) {
            return Enum.GetName(moves.GetType(), moves);
        }

        public static string ToString(this Types types, bool b) {
            return Enum.GetName(types.GetType(), types);
        }

        public static string ToString(this Abilities abilities, bool b) {
            return Enum.GetName(abilities.GetType(), abilities);
        }

        public static object[] Parse(Type t, string[] s) {
            List<object> x = new List<object>();
            foreach (string y in s) {
                x.Add(Enum.Parse(typeof(Moves), y));
            }
            return x.ToArray();
        }

        public static Object[] Parse(Type t, int[] i) {
            List<string> x = new List<string>();

            foreach (var y in i) {
                x.Add(y.ToString());
            }

            return Parse(t, x.ToArray());
        }

    }
}