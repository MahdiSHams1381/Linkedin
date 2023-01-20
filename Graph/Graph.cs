using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graph
{
   public class Graph : ADT_Graph
    {

        User Root;
        int size = 0;
        List<User> vertix = new List<User>(); // the key = name and value  = user for search it good
        List<Edge> Edge = new List<Edge>();
        public int Getsize()
        {
            //get size
            return this.size;
        }
        public List<User> AllUser()
        {
            //get all user
            List<User>? LIST_User_All = new List<User>();
            //foreach to add to list
            foreach (User User_Item in vertix)
            {
                LIST_User_All.Add(User_Item);
            }
            return LIST_User_All;
        }
        public void Add(int INT_ID)
        {
            //add when you dont have any name just with id
            vertix.Add(new User() { Id = INT_ID });
        }
        public User Add(User User_input)
         {
            User User_Find = FindElementById(User_input.Id);
            
            //if element is not being add them and after add connection of them and add ehe size
            if (User_Find == null)
            {
                //add element(User)
                vertix.Add(User_input );
                //add size
                size++;
                //add connection
                if (User_input.UserSpecialties != null)
                    foreach (Domain.SpecialtyUser User_item in User_input.UserSpecialties)
                    {
                        //if the conection is being in the vertix
                        if (FindElementById(User_item.UserId) != null)
                        {
                            Add(FindElementById(User_item.UserId));
                        }
                        //if the user is not being in the vertix list (the conection person in the json is not a complit user)
                        else
                        {
                            Add(User_item.UserId);
                        }

                        //Add edge
                        Edge.Add(new Edge() { UserFirst = User_input, UserLast = FindElementById(User_item.UserId) });
                    }

            }
            //if element is being just add conection (just added the size for conection that is not bing)
            else
            {
                //if the data of user is not complit
                if (User_Find.Name == null)
                {
                    User_Find.Name = User_input.Name;
                    User_Find.Field = User_input.Field;
                    User_Find.Profile = User_input.Profile;
                    User_Find.Id = User_input.Id;
                    User_Find.DateOfBirth= User_input.DateOfBirth;
                    User_Find.UniversityLocation= User_input.UniversityLocation;
                    User_Find.UserSpecialties = User_input.UserSpecialties;
                    User_Find.WorkPlace = User_input.WorkPlace;
                }
                //add connection
                if (User_input.UserSpecialties != null)
                    foreach (Domain.SpecialtyUser User_item in User_Find.UserSpecialties)
                    {
                        //Add vertix
                        if (FindElementById(User_item.UserId) != null)
                        {
                            Add(FindElementById(User_item.UserId));
                        }
                        //if the user is not being in the vertix list (the conection person in the json is not a complit user)
                        else
                        {
                            Add(User_item.UserId);
                        }
                        //Add edge

                        Edge.Add(new Edge() { UserFirst = User_Find, UserLast = FindElementById(User_item.UserId) });
                    }
            }
            return User_Find;

        }

        public User Clone()
        {
            throw new NotImplementedException();
        }

        public User FindElementById(int Id)
        {
            //search element by id
            //foreach to search in vertix
            foreach (User User_Item in vertix)
            {
                if (User_Item.Id == Id)
                {
                    return User_Item;
                }
            }

            return null;
        }

        public List<User> FindElementByName(string Name)
        {
            //search element by name
            //foreach to search in vertix
            List<User>? LIST_User_NameFound = new List<User>();
            foreach (User User_Item in vertix)
            {
                if (User_Item.Name == Name)
                {
                    LIST_User_NameFound.Add(User_Item);
                }
            }
            return LIST_User_NameFound;
        }

        public List<User> GetConnection(int INT_Id)
        {

            //return all connection from a vertix
            User User_Input = FindElementById(INT_Id);
            List<User>? LIST_User_Conection = new List<User>();
            if(User_Input != null)
            if(User_Input.UserSpecialties!= null)
            foreach (Domain.SpecialtyUser User_Item1 in User_Input.UserSpecialties)
            {
                LIST_User_Conection.Add(FindElementById(User_Item1.Id));
            }
            return LIST_User_Conection;
        }
        public User GetRoot()
        {
            //the person have max connection
            User User_Max = null;
            int INT_CounterMax = 0;
            foreach (User User_Item1 in vertix)
            {
                int INT_Counter1 = 0;
                foreach (Domain.SpecialtyUser User_Item2 in FindElementById(User_Item1.Id).UserSpecialties)
                {
                    INT_Counter1++;
                }
                if (INT_CounterMax < INT_Counter1)
                {
                    INT_CounterMax = INT_Counter1;
                    User_Max = User_Item1;
                }
            }
            return User_Max;
        }

        public User Remove(User User_Input)
        {

            //if user is not being throw the exception
            if (FindElementById(User_Input.Id) == null)
            {
                throw new Exception("the user is not found");
            }
            //if user is being remove them and all connection of them
            else
            {
                vertix.Remove(User_Input);
                foreach (User User_ToRemove in vertix)
                    Edge.Remove(new Edge() { UserFirst = User_Input, UserLast = User_ToRemove});
            }
            return User_Input;
        }
    }
}
