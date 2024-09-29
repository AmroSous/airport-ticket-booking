namespace AirportTicketBookingSystem.Utilities;

/// <summary>
/// Store set of ID's, 
/// and generates unique ID's. 
/// </summary>
/// <param name="initialValue"></param>
public class IdGenerator(int initialValue = 1)
{
    private int _currentId = initialValue;

    private readonly HashSet<int> _usedIds = [];

    public int Next
    {
        get
        {
            while (_usedIds.Contains(_currentId)) _currentId++;
            _usedIds.Add(_currentId);
            return _currentId;
        }
    }

    public void SetUsed(int id)
        => _usedIds.Add(id);

    public bool IsUsed(int id)
        => _usedIds.Contains(id);
}
