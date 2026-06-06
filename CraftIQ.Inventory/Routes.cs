namespace CraftIQ.Inventory
{
    public static class Routes
    {
        public const string BaseUrl = "api";
        public const string Products = BaseUrl + "/products";
        public const string Categories = BaseUrl + "/categories";
        public const string Orders = BaseUrl + "/Orders";
        public const string OrderDetails = BaseUrl + "/OrderDetails";
        public const string Inventories = BaseUrl + "/inventories";
        public const string Transactions = BaseUrl + "/transactions";


        public class CategoriesRoutes
        {
            public const string GetAll = Categories;
            public const string GetById = Categories + "/{id}";
            public const string ADD = Categories;
            public const string Update = Categories + "/{id}";
            public const string Delete = Categories + "/{id}";
        }

        public class InventoriesRoutes
        {
            public const string GetAll = Inventories;
            public const string GetById = Inventories + "/{id}";
            public const string ADD = Inventories;
            public const string Update = Inventories + "/{id}";
            public const string Delete = Inventories + "/{id}";
        }
        public class OrdersRoutes
        {
            public const string GetAll = Orders;
            public const string GetById = Orders + "/{id}";
            public const string ADD = Orders;
            public const string Update = Orders + "/{id}";
            public const string Delete = Orders + "/{id}";
        }
        public class ProductsRoutes
        {
            public const string GetAll = Products;
            public const string GetById = Products + "/{id}";
            public const string ADD = Products;
            public const string Update = Products + "/{id}";
            public const string Delete = Products + "/{id}";
        }
        public class OrderDetailsRoutes
        {
            public const string GetAll = OrderDetails;
            public const string GetById = OrderDetails + "/{id}";
            public const string ADD = OrderDetails;
            public const string Update = OrderDetails + "/{id}";
            public const string Delete = OrderDetails + "/{id}";
        }
        public class TransactionsRoutes
        {
            public const string GetAll = Transactions;
            public const string GetById = Transactions + "/{id}";
            public const string ADD = Transactions;
            public const string Update = Transactions + "/{id}";
            public const string Delete = Transactions + "/{id}";
        }
        public class AuthRoutes
        {
            public const string Login = BaseUrl + "/auth/login";
            public const string Register = BaseUrl + "/auth/register";
            public const string RefreshToken = BaseUrl + "/auth/refresh-token";
            public const string ForgotPassword = BaseUrl + "/auth/forgot-password";
            public const string ResetPassword = BaseUrl + "/auth/reset-password";

        }

    }
}
