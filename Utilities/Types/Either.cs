namespace Utilities.Types;

public class Either<TLeft, TRight>
{
    private readonly TLeft _left;
    private readonly TRight _right;

    public Either(TLeft left)
    {
        _left = left;
        _right = default;
        IsLeft = true;
    }

    public Either(TRight right)
    {
        _right = right;
        _left = default;
        IsLeft = false;
    }

    public bool IsLeft { get; }
    public bool IsRight => !IsLeft;

    public TLeft Left => _left;
    public TRight Right => _right;
}