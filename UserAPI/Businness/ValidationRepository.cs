using UserAPI.DataAccess;

namespace UserAPI.Businness
{
    public class ValidationRepository
    {
        private readonly ValidationDataAccessor dataAccessor;

        public ValidationRepository()
        {
            this.dataAccessor = new ValidationDataAccessor();
        }

        /// <summary>
        ///  Check user's validation code
        /// </summary>
        /// <returns>User</returns>
        public bool CheckValidationCode(int userid,string code)
        {
            return this.dataAccessor.CheckValidationCode(userid,code);
        }

        public bool InstertValidationCode(int userid, string code)
        {
            return this.dataAccessor.InsertValidationCode(userid, code);
        }

        public bool DeleteValidationCode(int userid)
        {
            return this.dataAccessor.DeleteValidationCode(userid);
        }

    }
}
