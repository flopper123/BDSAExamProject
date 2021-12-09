namespace LitExplore.Tests.Entity.Filter;

using System.Reflection;

using static ReflectionUtil;

abstract class AbsMock<T> {
    public abstract string GetName();
}

class SmallMock : AbsMock<int> {
    public static string Name = "SmallMock"; 
    override public string GetName() { return SmallMock.Name; }
}

class LargeMock : AbsMock<int> {
    public static string Name = "LargeMock";
    public override string GetName() { return LargeMock.Name; }
}

public class ReflectionUtilTests {
    Assembly assembly = Assembly.Load("LitExplore.Tests.Entity.Filter");
    
    [Fact]
    public void CanGetAllConcreteTypesIfTypeIsGeneric() {
        Type[] exp = { typeof(SmallMock), typeof(LargeMock) };
        var act = GetAllConcreteTypes(typeof(AbsMock<>), assembly);
        foreach (Type t in exp) {
            Assert.True(act.Contains(t), $"Failed find of type@{t} among concrete types..");
        }   
    }

    [Fact]
    public void CanGetAllConcreteTypes() {
        Type[] exp = { typeof(SmallMock), typeof(LargeMock) };
        var act = GetAllConcreteTypes(typeof(AbsMock<int>), assembly);
        foreach (Type t in exp) { 
                        Assert.True(act.Contains(t), $"Failed find of type@{t} among concrete types..");
        }   
    }
}