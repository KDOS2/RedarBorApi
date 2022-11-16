using RedarBorModels.Responses;

namespace RedarborDataBase.Users
{
    public static class DBAccesUser
    {
        /// <summary>
        /// gets user to acces token
        /// </summary>
        /// <returns>returns users</returns>
        public static async Task<List<UsersAccesModel>> GetUser()
        {
            List<UsersAccesModel> lstUsers = new List<UsersAccesModel>();
            UsersAccesModel user = new UsersAccesModel()
            {
                id = 1,
                numDoc = "10589200",
                nombreUser = "Admin RedarBor",
                apellidoUser = string.Empty,
                email = "redarborAdmin@redarbor.com",
                userlogin = "redarborAdmin",
                password = "123456"
            };
            lstUsers.Add(user);

            user = new UsersAccesModel()
            {
                id = 1,
                numDoc = "10000000",
                nombreUser = "Normal",
                apellidoUser = "User",
                email = "redarbor@redarbor.com",
                userlogin = "redarborUser",
                password = "0000"
            };

            lstUsers.Add(user);

            return lstUsers;
        }
    }
}
