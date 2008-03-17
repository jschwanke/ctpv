using System;
using System.Collections.Generic;
using System.Text;

namespace CTViewer.Model.Data
{
    /// <summary>
    /// Repräsentiert die Daten eines Patienten, welche in der ".hed" Datei stehen.
    /// </summary>
    public class Patient
    {
        private string firstname;
        private string lastname;
        private DateTime birthdate;

        /// <summary>
        /// Setzt oder gibt den Vornamen zurück.
        /// </summary>
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        /// <summary>
        /// Setzt oder gibt den Nachnamen zurück.
        /// </summary>
        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

        /// <summary>
        /// Setzt oder gibt das Geburtsdatum zurück.
        /// </summary>
        public DateTime Birthdate
        {
            get { return birthdate; }
            set { birthdate = value; }
        }
    }
}
