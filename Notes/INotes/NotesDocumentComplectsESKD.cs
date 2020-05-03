namespace INotes
{
    internal class NotesDocumentComplects : INotesRazdel
    {
        public string[] GetColumnNames()
        {
            return new string[] { "Формат", "Обозначение", "Наименование", "КодДокумента", "Примечание" };
        }

        public string GetDefaultValue()
        {
            return "1";
        }
    }
}