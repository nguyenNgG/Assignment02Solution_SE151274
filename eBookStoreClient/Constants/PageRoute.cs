namespace eBookStoreClient.Constants
{
    public static class PageRoute
    {
        // Home
        public static string Home = "/Index";

        // Books
        public static string Books = "/Books/Index";

        // Authors
        public static string Authors = "/Authors/Index";

        // Users
        public static string Users = "/Users/Index";
        public static string Login = "/Users/Login";
        public static string Logout = "/Users/Logout";
        public static string Profile = "/Users/Details";
        public static string EditProfile = "/Users/Edit";

        // Order
        public static string Orders = "/Orders/Index";
        public static string OrderPrepare = "/Orders/Prepare";

        // Cart
        public static string Cart = "/Cart/Details";
        public static string CartCreate = "/Carts/Create";
    }
}
