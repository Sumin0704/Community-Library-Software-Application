//CAB301 assessment 1 - 2022
//The implementation of MemberCollection ADT
using System;
using System.Linq;


class MemberCollection : IMemberCollection
{
    // Fields
    private int capacity;
    private int count;
    private Member[] members; //make sure members are sorted in dictionary order

    // Properties

    // get the capacity of this member colllection 
    // pre-condition: nil
    // post-condition: return the capacity of this member collection and this member collection remains unchanged
    public int Capacity { get { return capacity; } }

    // get the number of members in this member colllection 
    // pre-condition: nil
    // post-condition: return the number of members in this member collection and this member collection remains unchanged
    public int Number { get { return count; } }

   


    // Constructor - to create an object of member collection 
    // Pre-condition: capacity > 0
    // Post-condition: an object of this member collection class is created

    public MemberCollection(int capacity)
    {
        if (capacity > 0)
        {
            this.capacity = capacity;
            members = new Member[capacity];
            count = 0;
        }
    }

    // check if this member collection is full
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is full; otherwise return false.
    public bool IsFull()
    {
        return count == capacity;
    }

    // check if this member collection is empty
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is empty; otherwise return false.
    public bool IsEmpty()
    {
        return count == 0;
    }

    // Add a new member to this member collection
    // Pre-condition: this member collection is not full
    // Post-condition: a new member is added to the member collection and the members are sorted in ascending order by their full names;
    // No duplicate will be added into this the member collection
    public void Add(IMember member)
    {
        // To be implemented by students in Phase 1
        //Need to perform sorting
        bool isDuplicate = false;

        if (!IsFull() && count>0)
        {
            for(int i = 0; i < count; i++)
            {
                if(member.CompareTo(members[i]) == 0)
                {
                    //Console.WriteLine("Sorry Can't add a Duplicate member!");
                    isDuplicate = true;
                }
         
            }

            if (!isDuplicate)
            {
             
                //Console.WriteLine($"Let's Add the Member {member.FirstName} {member.LastName}");
                int i = count-1;
                while((i >= 0) && (member.CompareTo(members[i]) == -1))
                {
                    Member temp = members[i];
                    members[i] = members[i + 1];
                    members[i + 1] = temp;

                    i--;
                }
                members[i+1] = (Member)member;
                count++;
            }
        }
        else if(IsEmpty())
        {
            members[count] = (Member)member;
            count++;
            //Console.WriteLine($"The new member added is {member.FirstName} {member.LastName}");
        }
        
    }

    // Remove a given member out of this member collection
    // Pre-condition: nil
    // Post-condition: the given member has been removed from this member collection, if the given meber was in the member collection
    public void Delete(IMember aMember)
    {
        // Borrowed from Workshop questions (CustomerCollection) and modified
        int i =0;
       
        while((i<count) && (aMember.CompareTo(members[i]) != 0))
        {
            i++;
        }
        if (i == count)
        {
            Console.WriteLine("Couldn't find the member to delete.");
        }
        else
        {
            for (int j = i + 1; j < count; j++)
            {
                members[j - 1] = members[j];
            }
            count--;
        }
    }

    // Search a given member in this member collection 
    // Pre-condition: nil
    // Post-condition: return true if this memeber is in the member collection; return false otherwise; member collection remains unchanged
    public bool Search(IMember member)
    {
        // Borrowed from Lecture 3 slides and modified.
        int l = 0;
        int h = count-1;
        int mid = 0;
        while (l <= h)
        {
            mid = (l + h) / 2;
            if(member.CompareTo(members[mid]) == 0)
            {
                return true;
            }
            else if (member.CompareTo(members[mid]) == -1)
            {
                h = mid - 1;
            }
            else
            {
                l = mid + 1;
            }
        }
        return false;
    }

    // Find a given member in this member collection 
    // Pre-condition: nil
    // Post-condition: return the reference of the member object in the member collection, if this memeber is in the member collection; return null otherwise; member collection remains unchanged
    public IMember Find(IMember member)
    {
        if (Search(member))
        {
            for(int i = 0; i < count; i++)
            {
                if (members[i].FirstName == member.FirstName && members[i].LastName == member.LastName)
                {
                    return members[i];
                }
            }
        }  
        return null;    
    }

    // Remove all the members in this member collection
    // Pre-condition: nil
    // Post-condition: no member in this member collection 
    public void Clear()
    {
        for (int i = 0; i < count; i++)
        {
            this.members[i] = null;
        }
        count = 0;
    }

    // Return a string containing the information about all the members in this member collection.
    // The information includes last name, first name and contact number in this order
    // Pre-condition: nil
    // Post-condition: a string containing the information about all the members in this member collection is returned
    public string ToString()
    {
        string s = "";
        for (int i = 0; i < count; i++)
            s = s + members[i].ToString() + "\n";
        return s;
    }


}

