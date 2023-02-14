namespace Templ.WCustomers.ViewModels;
using Shared;
using Helpers;
using Services;

public class NavigationViewModel : BaseViewModel
{
    private readonly CustomerQueryParams qp = new();
    private readonly MainViewModel _mainViewModel;
    private string? lastMove;

    int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (_pageSize != value)
            {
                _pageSize = value;
                _mainViewModel.RefreshCustomers();
            }
        }
    }

    int _pageNo = 1;
    public int PageNo
    {
        get => _pageNo;
        set
        {
            _pageNo = value;
            IsCanMovePrev = _pageNo > 1;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsCanMovePrev));
        }
    }

    bool _isHasRecords;
    public bool IsHasRecords { 
        get=>_isHasRecords; 
        set
        {
            _isHasRecords = value;
            if(_isHasRecords && lastMove == "next")
            {
                PageNo++;
            }
            OnPropertyChanged();

            if ( lastMove == "top" )
            {
                PageNo = 1;
            }
            if( PageNo > 1 && lastMove== "prev" ) 
            { 
                PageNo--;
            }
        } 
    }

    public bool IsCanMovePrev { get; set; }

    string _sortByField = "None";
    public string SortByField
    {
        get => _sortByField;
        set 
        {
            if( value != _sortByField ) 
            {
                _sortByField = value;
                SortByDescendingEnebled = _sortByField != "None";
                OnPropertyChanged(nameof(SortByDescendingEnebled));

                PageNo = 1;
                _mainViewModel.RefreshCustomers();
            }
        }
    }

    bool _sortByDescending = false;
    public bool SortByDescending
    {
        get => _sortByDescending;
        set
        {
            if( value != _sortByDescending )
            {
                _sortByDescending = value;
                PageNo = 1;
                _mainViewModel.RefreshCustomers();
            }
        }
    }
    public bool SortByDescendingEnebled { get; set; }

    public NavigationViewModel(MainViewModel mainViewModel)
	{
		_mainViewModel= mainViewModel;
	}

    public CustomerQueryParams QueryParams(FilterCommandAgruments filterCommand)
    {
        switch (filterCommand.FiedName)
        {
            case "Name":
                qp.FilterName = filterCommand.FilterBy;
                break;
            case "CompanyName":
                qp.FilterCompanyName = filterCommand.FilterBy;
                break;
            case "Phone":
                qp.FilterPhone = filterCommand.FilterBy;
                break;
            case "Email":
                qp.FilterEmail = filterCommand.FilterBy;
                break;
        }
        
        qp.Top = 1;
        lastMove= "top";
        PageNo= 1;

        return qp;
    }

    public CustomerQueryParams QueryParams(string? move = null)
    {
        lastMove = move;
        var page = move switch
        {
            "top" => 1,
            "next" => _pageNo + 1,
            "prev" => _pageNo > 1 ? _pageNo - 1 : _pageNo,
            _ => _pageNo
        };

        qp.OrderBy = SortByField switch
        {
            "Name" => OrderByField.Name,
            "CompanyName" => OrderByField.CompanyName,
            "Phone" => OrderByField.Phone,
            "Email" => OrderByField.Email,
            _ => OrderByField.None
        };

        qp.OrderByDesc = _sortByDescending;

        qp.PageSize = _pageSize;
        qp.Top = (page - 1) * qp.PageSize + 1;
        return qp;
    }
}
