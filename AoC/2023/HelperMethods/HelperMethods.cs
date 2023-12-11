namespace AoC.HelperMethods
{
    public static class HelperMethods
    {
        public static string GetRelativePath(string folderName, string fileName)
        {
            return Path.Combine(folderName, fileName);
        }

    }
}


