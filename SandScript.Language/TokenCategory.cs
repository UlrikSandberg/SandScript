namespace SandScript.Language;

// Each of the token types must fit into a category
public enum TokenCategory
{
    None,
    Global,
    Trivia,
    Literal,
    Identifier,
    Punctuation,
    Arithmetic,
    Assignment,
    Logical,
    Bitwise
}