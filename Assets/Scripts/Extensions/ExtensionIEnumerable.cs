using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Extensions {
    public static class ExtensionIEnumerable {

        public static string[] ToString(this IEnumerable<int> ie) {
            return ie.ToArray().Select(t => t.ToString()).ToArray();
        }

    }
}
