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
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
     class Graph<E> : ADT_Graph
    {
      

        E Root;
        int size = 0;
        Hashtable vertix = new Hashtable(); // the key = name and value  = user for search it good
        Dictionary<User, User> Edge = new Dictionary<User, User>();
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
        public Dictionary<User, User> Alledge()
        {
            //get all edge (dictionary type)
            return Edge;
        }
        public void Add(int INT_ID)
        {
            //add when you dont have any name just with id
            vertix.Add(INT_ID, new User() { Id = INT_ID });
        }
        public User Add(User User_input)
        {
            User? User_Find = FindElementById(User_input.Id);
            //if element is not being add them and after add connection of them and add ehe size
            if (User_Find == null)
            {
                //add element(User)
                vertix.Add(User_input.Name, User_input);
                //add size
                size++;
                //add connection
                foreach (Domain.SpecialtyUser User_item in User_input.UserSpecialties)
                {
                    //if the conection is being in the vertix
                    if (FindElementById(User_item.UserId) == null)
                    {
                        Add(FindElementById(User_item.UserId));
                    }
                    //if the user is not being in the vertix list (the conection person in the json is not a complit user)
                    else
                    {
                        Add(User_item.UserId);
                    }

                    //Add edge
                    Edge.Add(User_input, Add(FindElementById(User_item.UserId)));
                }

            }
            //if element is being just add conection (just added the size for conection that is not bing)
            else
            {
                //if the data of user is not complit
                if (User_Find.Name == null)
                {
                    User_Find = User_input;
                }
                //add connection
                foreach (Domain.SpecialtyUser User_item in User_input.UserSpecialties)
                {
                    //Add vertix
                    Add(FindElementById(User_item.UserId));
                    //Add edge
                    Edge.Add(User_input, Add(FindElementById(User_item.UserId)));
                }
            }
            return User_input;

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

        public List<User> GetConnection(User User_Input)
        {
            //return all connection from a vertix
            List<User>? LIST_User_Conection = new List<User>();
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
                vertix.Remove(User_Input.Id);
                Edge.Remove(User_Input);
            }
            return User_Input;
        }
    }
}
