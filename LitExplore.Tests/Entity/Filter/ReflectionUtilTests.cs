namespace LitExplore.Tests.Entity.Filter;

using System.Reflection;

using static ReflectionUtil;

abstract class AbsMock<T> {
    public abstract string GetName();
}

class MediumMock : AbsMock<UInt64> {
    public UInt64 Id = 0;
    public static string Name = "MediumMock"; 
    override public string GetName() { return SmallMock.Name; }
}

class SmallMock : AbsMock<int> {
    public UInt64 id = 1;
    public static string Name = "SmallMock"; 
    override public string GetName() { return SmallMock.Name; }
}

class LargeMock : AbsMock<int> {
    public UInt64 id = 2;
    public static string Name = "LargeMock";
    public override string GetName() { return LargeMock.Name; }
}

public class ReflectionUtilTests {
    Assembly assembly = Assembly.Load("LitExplore.Tests");
    
    [Fact]
    public void CanGetAllConcreteTypes() {
        Type[] exp = { typeof(SmallMock), typeof(MediumMock), typeof(LargeMock) };
        var act = GetAllConcreteTypes(typeof(AbsMock<>), assembly);
        foreach (Type t in exp) {
            Assert.True(act.Contains(t), $"Failed find of type@{t} among concrete types..");
        }   
    }

    [Fact]
    public void GetConcreteTypes_Returns_CorrectCount() {
        Type[] exp = { typeof(SmallMock), typeof(MediumMock), typeof(LargeMock) };
        var act = GetAllConcreteTypes(typeof(AbsMock<>), assembly);
        Assert.Equal(act.Count(), exp.Count());
    }

    [Fact]
    public void StaticFieldAssertion_NotThrows_Correct()
    {
        var err = Record.Exception(
            () => StaticFieldAssertion(typeof(AbsMock<>), "Name", typeof(string), assembly)
        );
        Assert.Null(err);
    }
    
    [Fact]
    public void StaticFieldAssertion_Throws_MissingField_WrongName()
    {
        Assert.Throws<MissingFieldException>(
            () => StaticFieldAssertion(typeof(AbsMock<>), "NON_EXISTING", typeof(string), assembly)
        );
    }

    [Fact]
    public void StaticFieldAssertionThrows_TypeAccess_WrongType()
    {
        Assert.Throws<TypeAccessException>(
            () => StaticFieldAssertion(typeof(AbsMock<>), "Name", typeof(int), assembly)
        );
    }

    [Fact]
    public void StaticFieldAssertion_Throws_FieldAccess_NonStatic()
    {
        Assert.Throws<FieldAccessException>(
            () => StaticFieldAssertion(typeof(AbsMock<>), "Id", typeof(UInt64), assembly)
        );
    }
}