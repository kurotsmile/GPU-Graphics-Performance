// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("8U83KE1BBtoE0yRl4w4XkWmRHwWsOs3l74+koU481tQKIjd0/kU05+1uYG9f7W5lbe1ubm/8Qt3jXOW2qnQvFvjLkAbOh0TdlD+JVdkV20OREa3Sk3WmgHq01BlCozKN6Ey13rq21K2ns/LsXP4NMJ0duLOQuXoMDgV2Z3MS4LUVmnaT4+o/0pv4WCon5IdEqzPk+IUvB10Mske4NZ33IO2So73uoYBZYE8+aCIyw5Gd/ZLow7JKRKH+vMdZL/TFRJp6+6jOw0SX+H824A/iYfy7E5GKBzS/p+nAwY6EsWHW68mmzjlSpDwnekpCyjEeX+1uTV9iaWZF6SfpmGJubm5qb2znfIFs67+PW9x8nrbp3rHaUWXWjXGKXBl45qJOLG1sbm9u");
        private static int[] order = new int[] { 4,10,10,12,6,11,11,12,9,13,13,13,13,13,14 };
        private static int key = 111;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
