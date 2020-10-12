/*
 * Switcheroo - The incremental-search task switcher for Windows.
 * http://www.switcheroo.io/
 * Copyright 2009, 2010 James Sulak
 * Copyright 2014 Regin Larsen
 * 
 * Switcheroo is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Switcheroo is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Switcheroo.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace Switcheroo
{
    public class HotKey : ManagedWinapi.Hotkey
    {
        public string OpenWithHotKey { get; private set; }
        public string HideWins { get; private set; }
        public string ExePath { get; private set; }

        public void LoadSettings()
        {
            KeyCode = (System.Windows.Forms.Keys) Properties.Settings.Default.HotKey;
            WindowsKey = Properties.Settings.Default.WindowsKey;
            Alt = Properties.Settings.Default.Alt;
            Ctrl = Properties.Settings.Default.Ctrl;
            Shift = Properties.Settings.Default.Shift;
            OpenWithHotKey = Properties.Settings.Default.OpenWithHotKey;
            HideWins = Properties.Settings.Default.HideWins;
            ExePath = Properties.Settings.Default.ExePath;
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.HotKey = (int) KeyCode;
            Properties.Settings.Default.WindowsKey = WindowsKey;
            Properties.Settings.Default.Alt = Alt;
            Properties.Settings.Default.Ctrl = Ctrl;
            Properties.Settings.Default.Shift = Shift;

            Properties.Settings.Default.OpenWithHotKey = OpenWithHotKey ;
            Properties.Settings.Default.HideWins = HideWins;
            Properties.Settings.Default.ExePath = ExePath;
            Properties.Settings.Default.Save();
        }
    }
}