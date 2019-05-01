namespace Runtime.Tests
{
    public class ConflictFieldNameValueThenRefReferenceType : ProxyReferenceType
    {
        protected override void InitializeField()
        {
            AddField("fieldName", 1);
            AddField("fieldName", new ProxyReferenceType());
        }
    }
}
