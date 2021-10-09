﻿using GLC_Structs;
using System;

namespace GLC
{
    /// <summary>
    /// Panel implementation class for the game list on the library page
    /// </summary>
    public sealed class CGamesPanel : CPanel
    {
        string[] m_games;
        string m_currentPlatform;

        public CGamesPanel(int percentWidth, int percentHeight, CPage parentPage) : base("Games", PanelType.cGames, percentWidth, percentHeight, parentPage)
        {
            m_hoveredItemIndex = 0;
            m_currentPlatform = "";
            m_games = new string[]
            {
                "Game 1",
                "Game 2",
                "Game 3",
                "Game 4",
            };
        }

        public void Initalise()
        {

        }

#region CControl overrides
        public override void Redraw(bool fullRedraw)
        {
            if(!fullRedraw && m_currentPlatform.Length > 0 && m_isFocused)
            {
                string tmp = m_currentPlatform + " - " + m_games[m_hoveredItemIndex];

                int currentItemY = m_area.y + m_hoveredItemIndex + 1;
                CConsoleEx.WriteText(tmp, m_area.x + 1, currentItemY, CConstants.TEXT_PADDING_LEFT, m_area.width - 1, m_parentPage.GetColour(ColourThemeIndex.cPanelSelectFocusBG), m_parentPage.GetColour(ColourThemeIndex.cPanelSelectFocusFG));

                if(m_hoveredItemIndex > 0)
                {
                    tmp = m_currentPlatform + " - " + m_games[m_hoveredItemIndex - 1];
                    int adjecentItemY = m_area.y + m_hoveredItemIndex;
                    CConsoleEx.WriteText(tmp, m_area.x + 1, adjecentItemY, CConstants.TEXT_PADDING_LEFT, m_area.width - 1, m_parentPage.GetColour(ColourThemeIndex.cPanelMainBG), m_parentPage.GetColour(ColourThemeIndex.cPanelMainFG));
                }

                if(m_hoveredItemIndex < m_games.Length - 1)
                {
                    tmp = m_currentPlatform + " - " + m_games[m_hoveredItemIndex + 1];
                    int adjecentItemY = m_area.y + m_hoveredItemIndex + 2;
                    CConsoleEx.WriteText(tmp, m_area.x + 1, adjecentItemY, CConstants.TEXT_PADDING_LEFT, m_area.width - 1, m_parentPage.GetColour(ColourThemeIndex.cPanelMainBG), m_parentPage.GetColour(ColourThemeIndex.cPanelMainFG));
                }

                return;
            }

            CConsoleEx.DrawColourRect(m_area, ConsoleColor.Black);
            if(m_bottomBorder)
            {
                CConsoleEx.DrawHorizontalLine(m_area.x, m_area.height - 1, m_area.width, m_parentPage.GetColour(ColourThemeIndex.cPanelBorderBG), m_parentPage.GetColour(ColourThemeIndex.cPanelBorderFG));
            }
            if(m_rightBorder)
            {
                CConsoleEx.DrawVerticalLine(m_area.width - 1, m_area.y, m_area.height, m_parentPage.GetColour(ColourThemeIndex.cPanelBorderBG), m_parentPage.GetColour(ColourThemeIndex.cPanelBorderFG));
            }

            if(m_currentPlatform.Length == 0)
            {
                return;
            }

            for(int row = m_area.y + 1, i = 0; row < m_area.y + m_area.height && i < m_games.Length; row++, i++)
            {
                string tmp = m_currentPlatform + " - " + m_games[i];
                ColourThemeIndex background = (m_isFocused && m_hoveredItemIndex == i) ? ColourThemeIndex.cPanelSelectFocusBG : ColourThemeIndex.cPanelMainBG;
                ColourThemeIndex foreground = (m_isFocused && m_hoveredItemIndex == i) ? ColourThemeIndex.cPanelSelectFocusFG : ColourThemeIndex.cPanelMainFG;
                CConsoleEx.WriteText(tmp, m_area.x + 1, row, CConstants.TEXT_PADDING_LEFT, m_area.width - 1, m_parentPage.GetColour(background), m_parentPage.GetColour(foreground));
            }
        }

        public override void OnEnter()
        {
            throw new NotImplementedException();
        }

        public override void OnUpArrow()
        {
            m_hoveredItemIndex = Math.Max(m_hoveredItemIndex - 1, 0);
        }

        public override void OnDownArrow()
        {
            m_hoveredItemIndex = Math.Min(m_hoveredItemIndex + 1, m_games.Length - 1);
        }

        public override void OnLeftArrow()
        {
            throw new NotImplementedException();
        }

        public override void OnRightArrow()
        {
            throw new NotImplementedException();
        }
#endregion // CControl overrides

#region CPanel overrides
        protected override bool LoadContent()
        {
            throw new NotImplementedException();
        }

        protected override void ReloadContent()
        {
            throw new NotImplementedException();
        }

        protected override void DrawHighlighted(bool isFocused)
        {
            throw new NotImplementedException();
        }

        public override void OnTab()
        {
            throw new NotImplementedException();
        }

        public override void OnKeyInfo(ConsoleKeyInfo keyInfo)
        {
            throw new NotImplementedException();
        }

        public override bool Update()
        {
            throw new NotImplementedException();
        }

        public override void OnUpdateData(object sender, GenericEventArgs<string> e)
        {
            m_currentPlatform = e.Data;
            Redraw(true);
        }

        public override void OnSetFocus(object sender, GenericEventArgs<int> e)
        {
            m_isFocused = (e.Data == (int)m_panelType);
        }

        #endregion // CPanel overrides
    }
}
