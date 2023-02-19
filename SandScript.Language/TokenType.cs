namespace SandScript.Language;

// A discrete set of types of tokens
public enum TokenType
{
    // Global
    BOF,
    EOF,
    Invalid,
    Error,
    
    // Trivia
    Whitespace,
    LineComment,
    BlockComment,
    NewLine,
    
    // Literal
    StringLiteral,
    BooleanLiteral,
    IntegerLiteral,
    HexaDecimalNumericLiteral,
    OctaDecimalNumericLiteral,
    FloatingPointLiteral,
    
    // Identifier
    Identifier,
    Keyword,
    
    // Punctuation
    Dot,
    Comma,
    SemiColon,
    Colon,
    
    FatArrow,
    
    OpenParenthesis,
    CloseParenthesis,
    
    OpenSquareBracket,
    CloseSquareBracket,
    
    OpenCurlyBracket,
    CloseCurlyBracket,
    
    // Arithmetic
    Addition,
    Subtraction,
    Divide,
    Multiply,
    
    PlusPlus, // ++
    MinusMinus, // --
    MultMult, // **

    // Assignment = += -= *= 
    AssignmentAddition,
    AssignmentSubtraction,
    AssignmentMultiply,
    AssignmentDivide,
    Assignment,
    
    // Logical - && ||
    LogicalLess,
    LogicalLessOrEqual,
    LogicalGreater,
    LogicalGreaterOrEqual,
    LogicalEqual,
    LogicalNotEqual,
    LogicalAnd,
    LogicalOr,
    LogicalNot,
    
    // Bitwise
    BitwiseAnd,
    BitwiseOr,
    BitwiseXor,
}