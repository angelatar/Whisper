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
        public bool CheckValidationCode(string userEmail,string code)
        {
            return this.dataAccessor.CheckValidationCode(userEmail,code);
        }

        public bool InstertValidationCode(string userEmail)
        {
            var code = ValidationCode.CodeGenerator();
            if (ValidationCode.Send(userEmail, code))
                return this.dataAccessor.InsertValidationCode(userEmail, code);
            else
                return false;
        }

        public bool DeleteValidationCode(string userEmail)
        {
            return this.dataAccessor.DeleteValidationCode(userEmail);
        }

    }
}
