using System;
using System.Numerics;

using SFML.System;

using Latte.Core.Animation;


namespace Latte.Core.Type;


public class Vec2<T>(T x, T y) where T :
    IAdditionOperators<T, T, T>,
    ISubtractionOperators<T, T, T>,
    IMultiplyOperators<T, T, T>,
    IDivisionOperators<T, T, T>,
    IEqualityOperators<T, T, bool>,
    IConvertible
{
    public T X { get; set; } = x;
    public T Y { get; set; } = y;
    
    
    public static implicit operator Vector2f(Vec2<T> vec2) => new(vec2.X.ToSingle(null), vec2.Y.ToSingle(null));
    public static implicit operator Vector2i(Vec2<T> vec2) => new(vec2.X.ToInt32(null), vec2.Y.ToInt32(null));
    public static implicit operator Vector2u(Vec2<T> vec2) => new(vec2.X.ToUInt32(null), vec2.Y.ToUInt32(null));
    
    public static implicit operator Vec2f(Vec2<T> vec2) => new(vec2.X.ToSingle(null), vec2.Y.ToSingle(null));
    public static implicit operator Vec2i(Vec2<T> vec2) => new(vec2.X.ToInt32(null), vec2.Y.ToInt32(null));
    public static implicit operator Vec2u(Vec2<T> vec2) => new(vec2.X.ToUInt32(null), vec2.Y.ToUInt32(null));
    
    
    public static Vec2<T> operator+(Vec2<T> left, Vec2<T> right) => new(left.X + right.X, left.Y + right.Y);
    public static Vec2<T> operator-(Vec2<T> left, Vec2<T> right) => new(left.X - right.X, left.Y - right.Y);
    public static Vec2<T> operator*(Vec2<T> left, Vec2<T> right) => new(left.X * right.X, left.Y * right.Y);
    public static Vec2<T> operator/(Vec2<T> left, Vec2<T> right) => new(left.X / right.X, left.Y / right.Y);
    
    public static Vec2<T> operator*(Vec2<T> left, T right) => new(left.X * right, left.Y * right);
    public static Vec2<T> operator/(Vec2<T> left, T right) => new(left.X / right, left.Y / right);
    
    public static bool operator==(Vec2<T> left, Vec2<T> right) => left.Equals(right);
    public static bool operator!=(Vec2<T> left, Vec2<T> right) => !left.Equals(right);


    public bool Equals(Vec2<T> other)
        => X == other.X && Y == other.Y;

    public override bool Equals(object? obj)
        => obj is not null && Equals((Vec2<T>)obj);


    public override int GetHashCode() => HashCode.Combine(X, Y);


    public override string ToString() => $"Vec2<{typeof(T).Name}>({X}, {Y})";
}


public class Vec2f(float x = default, float y = default) : Vec2<float>(x, y), IAnimateable<Vec2f>
{
    public static implicit operator Vec2f(Vector2f vec2) => new(vec2.X, vec2.Y);
    
    
    public AnimationState AnimateThis(Vec2f to, float time, EasingType easingType = EasingType.Linear)
        => Animate.Vec2f(this, to, time, easingType);

    public Vec2f AnimationValuesToThis(float[] values)
        => values.ToVec2f();
}


public class Vec2i(int x = default, int y = default) : Vec2<int>(x, y), IAnimateable<Vec2i>
{
    public static implicit operator Vec2i(Vector2i vec2) => new(vec2.X, vec2.Y);
    

    public AnimationState AnimateThis(Vec2i to, float time, EasingType easingType = EasingType.Linear)
        => Animate.Vec2i(this, to, time, easingType);

    public Vec2i AnimationValuesToThis(float[] values)
        => values.ToVec2i();
}


public class Vec2u(uint x = default, uint y = default) : Vec2<uint>(x, y), IAnimateable<Vec2u>
{
    public static implicit operator Vec2u(Vector2u vec2) => new(vec2.X, vec2.Y);
    
    
    public AnimationState AnimateThis(Vec2u to, float time, EasingType easingType = EasingType.Linear)
        => Animate.Vec2u(this, to, time, easingType);

    public Vec2u AnimationValuesToThis(float[] values)
        => values.ToVec2u();
}