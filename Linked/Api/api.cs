
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain;

namespace Linked.Api
{
    public class api
    {
        public void FUNC_AddPersonToDataBase(User User)
        {
            //add person to data base
        }
        public User FUNC_SearchUser(int INT_UserID)
        {
            // find the person with this id from data base
            return new User();
        }

        public List<User> FUNC_findeThePersonConection(User User_BaseUser)
        {
            List<User> LIST_User_toReturn = new List<User>();
            //foreach (User item1 in User_BaseUser.connectionId)
            //{
            //    LIST_User_toReturn.Add(item1);
            //    foreach (User item2 in item1.connectionId)
            //    {
            //        LIST_User_toReturn.Add(item2);
            //        foreach (User item3 in item2.connectionId)
            //        {
            //            LIST_User_toReturn.Add(item3);
            //            foreach (User item4 in item3.connectionId)
            //            {
            //                LIST_User_toReturn.Add(item4);
            //                foreach (User item5 in item4.connectionId)
            //                {
            //                    LIST_User_toReturn.Add(item5);
            //                    foreach (User item6 in item5.connectionId)
            //                    {
            //                        LIST_User_toReturn.Add(item6);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            return LIST_User_toReturn;
        }
    }
}
