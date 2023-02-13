using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Templ.WCustomers.Shared;

public class BaseDomainViewModel : BaseViewModel, INotifyDataErrorInfo
{
    protected Dictionary<string, List<string>> propErrors = new();

    public bool HasErrors => propErrors.Any();

    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName != null)
        {
            return propErrors.ContainsKey(propertyName) ? propErrors[propertyName] : null!;
        }
        return null!;
    }

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    protected void AddError(string propertyName, string error)
    {
        if (!propErrors.ContainsKey(propertyName))
            propErrors[propertyName] = new List<string>();

        if (!propErrors[propertyName].Contains(error))
        {
            propErrors[propertyName].Add(error);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
    protected void ClearErrors(string propertyName)
    {
        if (propErrors.ContainsKey(propertyName))
        {
            propErrors.Remove(propertyName);
        }
    }
}
