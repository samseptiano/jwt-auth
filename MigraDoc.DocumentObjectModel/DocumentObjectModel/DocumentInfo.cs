#region MigraDoc - Creating Documents on the Fly
//
// Authors:
//   Stefan Lange
//   Klaus Potzesny
//   David Stephensen
//
// Copyright (c) 2001-2017 empira Software GmbH, Cologne Area (Germany)
//
// http://www.pdfsharp.com
// http://www.migradoc.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using MigraDoc.DocumentObjectModel.publics;

namespace MigraDoc.DocumentObjectModel
{
    /// <summary>
    /// Contains information about document content, author etc.
    /// </summary>
    public class DocumentInfo : DocumentObject
    {
        /// <summary>
        /// Initializes a new instance of the DocumentInfo class.
        /// </summary>
        public DocumentInfo()
        { }

        /// <summary>
        /// Initializes a new instance of the DocumentInfo class with the specified parent.
        /// </summary>
        public DocumentInfo(DocumentObject parent) : base(parent) { }

        #region Methods
        /// <summary>
        /// Creates a deep copy of this object.
        /// </summary>
        public new DocumentInfo Clone()
        {
            return (DocumentInfo)DeepCopy();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the document title.
        /// </summary>
        public string Title
        {
            get { return _title.Value; }
            set { _title.Value = value; }
        }
        [DV]
        public NString _title = NString.NullValue;

        /// <summary>
        /// Gets or sets the document author.
        /// </summary>
        public string Author
        {
            get { return _author.Value; }
            set { _author.Value = value; }
        }
        [DV]
        public NString _author = NString.NullValue;

        /// <summary>
        /// Gets or sets keywords related to the document.
        /// </summary>
        public string Keywords
        {
            get { return _keywords.Value; }
            set { _keywords.Value = value; }
        }
        [DV]
        public NString _keywords = NString.NullValue;

        /// <summary>
        /// Gets or sets the subject of the document.
        /// </summary>
        public string Subject
        {
            get { return _subject.Value; }
            set { _subject.Value = value; }
        }
        [DV]
        public NString _subject = NString.NullValue;

        /// <summary>
        /// Gets or sets a comment associated with this object.
        /// </summary>
        public string Comment
        {
            get { return _comment.Value; }
            set { _comment.Value = value; }
        }
        [DV]
        public NString _comment = NString.NullValue;
        #endregion

        #region public
        /// <summary>
        /// Converts DocumentInfo into DDL.
        /// </summary>
        public override void Serialize(Serializer serializer)
        {
            serializer.WriteComment(_comment.Value);
            int pos = serializer.BeginContent("Info");

            if (Title != String.Empty)
                serializer.WriteSimpleAttribute("Title", Title);

            if (Subject != String.Empty)
                serializer.WriteSimpleAttribute("Subject", Subject);

            if (Author != String.Empty)
                serializer.WriteSimpleAttribute("Author", Author);

            if (Keywords != String.Empty)
                serializer.WriteSimpleAttribute("Keywords", Keywords);

            serializer.EndContent(pos);
        }

        /// <summary>
        /// Returns the meta object of this instance.
        /// </summary>
        public override Meta Meta
        {
            get { return _meta ?? (_meta = new Meta(typeof(DocumentInfo))); }
        }
        static Meta _meta;
        #endregion
    }
}