
using Frosty.Domain.Shared;

namespace Frosty.Domain.Users;

public class Firstname();



// NOTE: Plan for VO

// TODO: Create a validate function for Email in EmailValueObject<>
// This will also be in shared because we have email in multiple entities
// when creating the entity
// Pattern match with regex for proper email

// Match the domain part of the email for Records with the domain (Record only)
// and therefore, only in the Record Email Value Object

// TODO: Website Value object in record needs a validate function
// Regex match standard website syntax.

// TODO: Emailverifyid is currently a string. Change.
// Add functionality to randomly generate an ID of 8 chars.

// TODO: Comment should have the Add Comment, remove comment functionality.
// Also, validate that a comment name or value is not blank
// Comment value should be a 

// TODO: Then, check the other objects. Deal with these first.
