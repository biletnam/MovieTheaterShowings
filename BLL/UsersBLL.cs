using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharedResources.Interfaces;
using SharedResources.Exceptions.BLL;
using SharedResources.Exceptions.DAL;

namespace BLL
{
    public class UsersBLL : IUsersBLL
    {
        private IUsersDAL usersDAL { get; set; }

        public UsersBLL(IUsersDAL _usersDAL)
        {
            usersDAL = _usersDAL;
        }

        public IUserMapper Insert(IUserMapper user) 
        {
            IUserMapper output = null;
            
            //Check for null or empty values:
            var testVars = new Object[]{
                user.Name,
                user.RoleName,
                user.password_hash
            };

            //Use the plain text string stored in user.password_hash, and then hash it and reset it back into user.password_hash.
            user.password_hash = bcrypt_hashing.HashPassword(user.password_hash);

            if (!DataValidator.is_null_empty_or_zero(testVars))
            {
                try
                {
                    output = usersDAL.Insert(user);
                }
                catch (SqlDALException e)
                {
                    throw new SqlBLLException("One or more SQL constraints may have caused this issue.  Please provide the Name, RoleName, and password_hash fields.  Please be sure the Name field is unique and all other data is valid.", e);
                }
            }
            else
            {
                throw new MissingDataBLLException("Cannot complete this operation because required data is missing.  Please be sure to provide Name and password_hash.");
            }

            return output;
        }

        public IUserMapper Get_User_by_User_Name(IUserMapper user) 
        {

            IUserMapper output = null;
            //Check for null or empty values:
            var testVars = new Object[]{
                user.Name
            };
            
            if (!DataValidator.is_null_empty_or_zero(testVars))
            {
                try
                {
                    output = usersDAL.Get_User_by_User_Name(user);
                }
                catch (SqlDALException e)
                {
                    throw new SqlBLLException("One or more SQL constraints may have caused this issue.  Please provide the Name field.  The Name must exist in the database.", e);
                }
            }
            else
            {
                throw new MissingDataBLLException("Cannot complete this operation because required data is missing.  Please be sure to provide the Name field.");
            }

            return output;
        }

        public bool authenticate_user(IUserMapper user)
        {
            bool output = false; //Create an output variable.

            //Check for null or empty values:
            var testVars = new Object[]{
                user.Name,
                user.password_hash
            };
            
            if (!DataValidator.is_null_empty_or_zero(testVars))
            {
                try
                {
                    IUserMapper found_user = usersDAL.Get_User_by_User_Name(user);
                    if(found_user != null){
                        if (bcrypt_hashing.ValidatePassword(user.password_hash, found_user.password_hash))
                        {
                            output = true;
                        }
                    }
                }
                catch (SqlDALException e)
                {
                    throw new SqlBLLException("One or more SQL constraints may have caused this issue.  Please provide the Name and password_hash field.  The Name must exist in the database.", e);
                }
            }
            else
            {
                throw new MissingDataBLLException("Cannot complete this operation because required data is missing.  Please be sure to provide the Name and password_hash fields.");
            }
            
            return output;
        }

    }
}
