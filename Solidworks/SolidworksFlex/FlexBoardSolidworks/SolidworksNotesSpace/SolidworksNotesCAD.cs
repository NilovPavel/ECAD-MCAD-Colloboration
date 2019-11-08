using SolidWorks.Interop.sldworks;
using System;
using System.Linq;
namespace SolidWorksAssemblySpace
{
    class SolidworksNotesCAD : INotesCAD
    {
        private ModelDoc2 modelDoc;
        private CommentFolder commentFolder;

        string[] INotesCAD.GetRazdelNames()
        {
            return new string[] { "Документация", "Комплекты", "Материалы", "Документация ЭМ", "Комплекты ЭМ", "Материалы ЭМ" };
        }

        string[] INotesCAD.ReadRazdelNotes(string razdelName)
        {
            Comment comment = this.GetCommentByRazdelName(razdelName);
            string[] razdelNotesCollection = comment.Text.Split("\r\n".ToCharArray(), StringSplitOptions.None);
            return razdelNotesCollection;
        }

        void INotesCAD.WriteNotes(Notes notes)
        {
            Comment comment = this.GetCommentByRazdelName(notes.RazdelName);
            comment.Text = string.Join("\r\n", notes.RazdelNotesCollection);
            this.modelDoc.ForceRebuild3(true);
        }

        private Comment CreateNote(string noteName)
        {
            Comment comment = this.commentFolder.AddComment(string.Empty);
            comment.Name = noteName;
            return comment;
        }

        private Comment GetCommentByRazdelName(string razdelName)
        {
            Comment commentRazdelNote = default(Comment);
            object[] vComments = (object[])this.commentFolder.GetComments();
            if (vComments != null)
                commentRazdelNote = (Comment)vComments.Where(itemNote => ((Comment)itemNote).Name.Equals(razdelName)).FirstOrDefault();
            commentRazdelNote = commentRazdelNote ?? this.CreateNote(razdelName);
            return commentRazdelNote;
        }

        private CommentFolder GetCommentsFolder()
        {
            for (Feature swFeat = (Feature)this.modelDoc.FirstFeature(); swFeat != null; swFeat = (Feature)swFeat.GetNextFeature())
                if (swFeat.GetTypeName() == "CommentsFolder")
                    return (CommentFolder)swFeat.GetSpecificFeature2();
            return default(CommentFolder);
        }

        private void Initialization()
        {
            this.commentFolder = this.GetCommentsFolder();
        }

        public SolidworksNotesCAD(ModelDoc2 modelDoc)
        {
            this.modelDoc = modelDoc;
            this.Initialization();
        }
    }
}