
using Frosty.Domain.Shared;
namespace Frosty.Domain.Users;

public class Firstname : Name {

    public Firstname(string name)
        : base(name) {
    }
}


// NOTE: Plan for VO

// TODO: Create a NameValueObject<T> class to be inhereted
// // Name will have a method called MakeProper
// // Name will have a function called validate.
// // => Check blank. Check less that 3 chars.
// All name value objects will inheret this

// Put this in the shared folder

// TODO: Create a validate function for Email in EmailValueObject<>
// This will also be in shared because we have email in multiple entities
// when creating the entity
// Pattern match with regex for proper email

// Match the domain part of the email for Records with the domain (Record only)
// and therefore, only in the Record Email Value Object

// TODO: ContactInfo has strings for all internal fields. Create FN, LN and Email Value Objects for Record.

// TODO: Website Value object in record needs a validate function
// Regex match standard website syntax.

// TODO: Emailverifyid is currently a string. Change.
// Add functionality to randomly generate an ID of 8 chars.

// TODO: Comment should have the Add Comment, remove comment functionality.
// Also, validate that a comment name or value is not blank
// Comment value should be a 

// TODO: Then, check the other objects. Deal with these first.


// TODO: Next, sit trhou
