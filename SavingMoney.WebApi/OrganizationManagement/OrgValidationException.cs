namespace SavingMoney.WebApi.OrganizationManagement;

[Serializable]
public class OrgValidationException : Exception
{
    public OrgValidationModel ValidationModel { get; init; }

    public OrgValidationException(OrgValidationModel validationModel) : base("There are validation errors in the model")
    {
        ValidationModel = validationModel;
    }
}