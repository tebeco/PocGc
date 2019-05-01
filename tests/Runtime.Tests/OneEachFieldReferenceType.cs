namespace Runtime.Tests
{
    public class OneEachFieldReferenceType : ProxyReferenceType
    {
        protected override void InitializeField()
        {
            AddField("valueFieldName", 1);
            AddField("refFieldName", new ProxyReferenceType());
        }
    }
}
