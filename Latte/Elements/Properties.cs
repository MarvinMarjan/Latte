using Latte.Core.Animation;
using Latte.Elements.Primitives;


namespace Latte.Elements;


public abstract class Property
{
    public Element Owner { get; }
    
    public string Name { get; }
    public object Value { get; set; }
    
 
    public Property(Element owner, string name, object value)
    {
        Owner = owner;
        Name = name;
        Value = value;
        
        Owner.Properties.Add(Name, this);
    }
    
    
    public void Set(object value) => Value = value;
    public object Get() => Value;
}


public class Property<T>(Element owner, string name, T value) : Property(owner, name, value)
    where T : notnull
{
    public new T Value
    {
        get => (T)base.Value;
        set => base.Value = value;
    }
    
    
    public void Set(T value) => Value = value;
    public new T Get() => Value;
    
    
    public static implicit operator T(Property<T> property) => property.Get();
}




public abstract class AnimatableProperty(Element owner, string name, object value) : Property(owner, name, value)
{
    public new IAnimatable Value
    {
        get => (IAnimatable)base.Value;
        set => base.Value = value;
    }
    
    public AnimationState? AnimationState { get; protected set; }
    
    
    public void Animate(object to, double time, EasingType easingType = EasingType.Linear)
    {
        AnimationState?.Finish();

        AnimationState = Value.AnimateThis(to, time, easingType);
        AnimationState.Updated += (_, args) => Value = Value.AnimationValuesToThis(args.CurrentValues);
        AnimationState.Finished += (_, _) => AnimationState = null;
        
        Owner.AddPropertyAnimation(AnimationState);
    }
}


public class AnimatableProperty<T>(Element owner, string name, T value) : AnimatableProperty(owner, name, value)
    where T : IAnimatable<T>
{
    public new T Value
    {
        get => (T)base.Value;
        set => base.Value = value;
    }
    
    
    public void Set(T value) => Value = value;
    public new T Get() => Value;
    
    
    public static implicit operator T(AnimatableProperty<T> property) => property.Get();
    
    
    public void Animate(T to, double time, EasingType easingType = EasingType.Linear)
        => base.Animate(to, time, easingType);
    
}