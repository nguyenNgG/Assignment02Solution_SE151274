namespace eBookStoreClient.Constants
{
    public static class PageRoute
    {
        // Home
        public static string Home = "/Index";

        // Books
        public static string Books = "/Books/Index";
        public static string BooksPrepare = "/Books/Prepare";

        // Authors
        public static string Authors = "/Authors/Index";

        // Publishers
        public static string Publishers = "/Publishers/Index";

        // Users
        public static string Users = "/Users/Index";
        public static string Login = "/Users/Login";
        public static string Logout = "/Users/Logout";
        public static string Profile = "/Users/Details";
        public static string EditProfile = "/Users/Edit";

        // Cart
        public static string Cart = "/BookAuthors/Details";
        public static string CartCreate = "/BookAuthors/Create";
    }
}
