using System;

public class IteratorSearchElement : IIteratorAction
{
    private string uniqueID;
    private RecordESKD recordESKD;
    private bool isEnd;

    public IteratorSearchElement(string uniqueID)
    {
        this.uniqueID = uniqueID;
    }

    void IIteratorAction.ElementAction(RecordESKD recordESKD)
    {
        if (this.isEnd)
            return;
        string[] uids = recordESKD.CadID.Split('$');
        int index = Array.IndexOf(uids, uniqueID);
        if (index != -1)
        {
            this.isEnd = true;
            this.recordESKD = recordESKD;
        }
    }

    public RecordESKD GetRecordESKD()
    {
        return this.recordESKD;
    }

    void IIteratorAction.RazdelAction(Spisok spisok)
    {
        return;
    }
}