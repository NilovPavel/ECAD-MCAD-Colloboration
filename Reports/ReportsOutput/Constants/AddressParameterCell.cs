public class AddressParameterCell
{
    public int Row
    { get; private set; }
    public int Column
    { get; private set; }
    public string Parametr
    { get; private set; }

    public AddressParameterCell(int column, int row, string Parametr)
    {
        this.Row = row;
        this.Column = column;
        this.Parametr = Parametr;
    }
}