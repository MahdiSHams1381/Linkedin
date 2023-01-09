using Linked.Models.User;
using Linked.Models.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Linked.Api
{
    public class api
    {
        public void FUNC_AddPersonToDataBase(user User)
        {
            //add person to data base
        }
        public user FUNC_SearchUser(int INT_UserID)
        {
            // find the person with this id from data base
            return new user();
        }

        public List<user> FUNC_findeThePersonConection(user User_BaseUser)
        {
            List<user> LIST_User_toReturn = new List<user>();
            foreach (user item1 in User_BaseUser.connectionId)
            {
                LIST_User_toReturn.Add(item1);
                foreach (user item2 in item1.connectionId)
                {
                    LIST_User_toReturn.Add(item2);
                    foreach (user item3 in item2.connectionId)
                    {
                        LIST_User_toReturn.Add(item3);
                        foreach (user item4 in item3.connectionId)
                        {
                            LIST_User_toReturn.Add(item4);
                            foreach (user item5 in item4.connectionId)
                            {
                                LIST_User_toReturn.Add(item5);
                                foreach (user item6 in item5.connectionId)
                                {
                                    LIST_User_toReturn.Add(item6);
                                }
                            }
                        }
                    }
                }
            }
            return LIST_User_toReturn;
        }
    }
}
