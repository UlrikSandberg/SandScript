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
    QuestionMark,
    
    FatArrow,
    
    OpenParenthesis,
    CloseParenthesis,
    
    OpenSquareBracket,
    CloseSquareBracket,
    
    OpenCurlyBracket,
    CloseCurlyBracket,
    
    // Arithmetic + - / * %
    Addition,
    Subtraction,
    Divide,
    Multiply,
    Modulo,
    
    PlusPlus, // ++
    MinusMinus, // --
    MultMult, // **

    // Assignment: = += -= *= /= %=
    AssignmentAddition,
    AssignmentSubtraction,
    AssignmentMultiply,
    AssignmentDivide,
    AssignmentModulo,
    Assignment,
    
    // Logical: < <= > >= == != || && !
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
    BitwiseShiftLeft,
    BitwiseShiftRight
}