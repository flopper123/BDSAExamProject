namespace LitExplore.Entity.Entities;

//! Because stupid entityFramework dont save collections of primitives you wil build it yourself.
//! 2 ways either have the Keywords as an entity, or do some text parsing and save it as a string.
public class KeyWord
{
    [Key]
    public string Keyword {get; set;} = "?"; 

    public Publication? Publication {get; set;}
}