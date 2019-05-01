namespace Runtime.Tests
{
    public class ConflictFieldNameRefThenValueReferenceType : ProxyReferenceType
    {
        protected override void InitializeField()
        {
            AddField("fieldName", new ProxyReferenceType());
            AddField("fieldName", 1);
        }
    }
}
