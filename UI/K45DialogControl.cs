﻿using ColossalFramework.DataBinding;
using ColossalFramework.UI;
using Klyte.Commons.Extensors;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Klyte.Commons.Utils
{
    internal class K45DialogControl : UICustomControl
    {
        public const string PANEL_ID = "K45Dialog";

        #region Panel composition
        public static UIDynamicPanels.DynamicPanelInfo CreatePanelInfo(UIView view)
        {

            KlyteMonoUtils.CreateUIElement(out UIPanel mainPanel, null, PANEL_ID);
            mainPanel.enabled = false;
            mainPanel.maximumSize = new Vector2(800, view.fixedHeight - 300);
            mainPanel.minimumSize = new Vector2(800, 70);
            mainPanel.backgroundSprite = "MenuPanel2";
            mainPanel.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.CenterVertical;
            mainPanel.autoLayout = true;
            mainPanel.autoFitChildrenVertically = true;
            mainPanel.autoLayoutDirection = LayoutDirection.Vertical;
            mainPanel.autoLayoutStart = LayoutStart.TopLeft;
            mainPanel.autoLayoutPadding = new RectOffset(0, 0, 0, 10);

            #region Title
            KlyteMonoUtils.CreateUIElement(out UIPanel titleContainer, mainPanel.transform, "TitleContainer");
            titleContainer.size = new Vector2(800, 40);


            KlyteMonoUtils.CreateUIElement(out UILabel title, titleContainer.transform, "Title");
            title.text = "<k45symbol K45_HexagonIcon_NOBORDER,5e35b1,K> Klyte45";
            title.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.CenterVertical;
            title.minimumSize = new Vector3(titleContainer.width - 100, 0);
            title.textScale = 2;
            title.textAlignment = UIHorizontalAlignment.Center;



            KlyteMonoUtils.CreateUIElement(out UISprite modIcon, titleContainer.transform, "ModIcon", new Vector4(5, 5, 32, 32));

            KlyteMonoUtils.CreateUIElement(out UIButton closeButton, titleContainer.transform, "CloseButton");
            closeButton.pivot = UIPivotPoint.TopRight;
            closeButton.area = new Vector4(mainPanel.width - 5, 3, 32, 32);
            closeButton.normalBgSprite = "buttonclose";
            closeButton.hoveredBgSprite = "buttonclosehover";
            closeButton.pressedBgSprite = "buttonclosepressed";


            #endregion

            #region Text area
            KlyteMonoUtils.CreateUIElement(out UILabel boxText, mainPanel.transform, "BoxText");
            boxText.minimumSize = new Vector2(800, 60);
            boxText.wordWrap = true;
            boxText.autoSize = false;
            boxText.autoHeight = true;
            boxText.processMarkup = true;
            boxText.padding = new RectOffset(10, 10, 5, 5);
            boxText.textAlignment = UIHorizontalAlignment.Center;
            boxText.verticalAlignment = UIVerticalAlignment.Middle;
            #endregion

            #region Action Buttons
            KlyteMonoUtils.CreateUIElement(out UIPanel buttonContainer, mainPanel.transform, "ButtonSupContainer");
            buttonContainer.size = new Vector2(800, 70);
            buttonContainer.autoFitChildrenVertically = true;

            KlyteMonoUtils.CreateUIElement(out UIPanel buttonSubContainer, buttonContainer.transform, "ButtonContainer");
            buttonSubContainer.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.Top;
            buttonSubContainer.pivot = UIPivotPoint.TopCenter;
            buttonSubContainer.autoFitChildrenVertically = true;
            buttonSubContainer.autoFitChildrenHorizontally = true;
            buttonSubContainer.autoLayout = true;
            buttonSubContainer.autoLayoutPadding = new RectOffset(5, 5, 0, 0);
            buttonSubContainer.relativePosition = new Vector3(0, 0);

            KlyteMonoUtils.CreateUIElement(out UIButton button1, buttonSubContainer.transform, "ButtonAction1");
            button1.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.Top;
            button1.size = new Vector2(150, 60);
            button1.text = "AAAAA";
            button1.wordWrap = true;
            KlyteMonoUtils.InitButtonFull(button1, false, "ButtonMenu");
            KlyteMonoUtils.CreateUIElement(out UIButton button2, buttonSubContainer.transform, "ButtonAction2");
            button2.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.Top;
            button2.size = new Vector2(150, 60);
            button2.text = "BBBBB";
            button2.wordWrap = true;
            KlyteMonoUtils.InitButtonFull(button2, false, "ButtonMenu");
            KlyteMonoUtils.CreateUIElement(out UIButton button3, buttonSubContainer.transform, "ButtonAction3");
            button3.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.Top;
            button3.size = new Vector2(150, 60);
            button3.text = "CCCCC";
            button3.wordWrap = true;
            KlyteMonoUtils.InitButtonFull(button3, false, "ButtonMenu");
            KlyteMonoUtils.CreateUIElement(out UIButton button4, buttonSubContainer.transform, "ButtonAction4");
            button4.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.Top;
            button4.size = new Vector2(150, 60);
            button4.text = "DDDDD";
            button4.wordWrap = true;
            KlyteMonoUtils.InitButtonFull(button4, false, "ButtonMenu");
            #endregion

            #region Bindings creation
            mainPanel.gameObject.AddComponent<K45DialogControl>();
            BindPropertyByKey bindByKey = mainPanel.gameObject.AddComponent<BindPropertyByKey>();
            bindByKey.m_Bindings.AddRange(new List<BindPropertyByKey.BindingInfo>(){
                CreateBind("title"          ,title, "text"),
                CreateBind("icon"           ,modIcon, "spriteName"),
                CreateBind("showClose"          ,closeButton, "isVisible"),
                CreateBind("message"            ,boxText, "text"),
                CreateBind("messageAlign",boxText, "textAlignment"),
                CreateBind("showButton1"        ,button1,"isVisible"),
                CreateBind("showButton2"        ,button2,"isVisible"),
                CreateBind("showButton3"        ,button3,"isVisible"),
                CreateBind("showButton4"        ,button4,"isVisible"),
                CreateBind("textButton1"        ,button1,"text"),
                CreateBind("textButton2"        ,button2,"text"),
                CreateBind("textButton3"        ,button3,"text"),
                CreateBind("textButton4"        ,button4,"text"),
            });
            #endregion

            #region Declare Dynamic Panel Info
            var panelTestInfo = new UIDynamicPanels.DynamicPanelInfo();
            panelTestInfo.GetType().GetField("m_Name", RedirectorUtils.allFlags).SetValue(panelTestInfo, PANEL_ID);
            panelTestInfo.GetType().GetField("m_PanelRoot", RedirectorUtils.allFlags).SetValue(panelTestInfo, mainPanel);
            panelTestInfo.GetType().GetField("m_IsModal", RedirectorUtils.allFlags).SetValue(panelTestInfo, true);
            #endregion

            return panelTestInfo;
        }

        private static BindPropertyByKey.BindingInfo CreateBind(string key, UIComponent component, string property)
        {
            return new BindPropertyByKey.BindingInfo()
            {
                key = key,
                target = new BindingReference()
                {
                    component = component,
                    memberName = property
                }
            };
        }
        #endregion

        public void Awake()
        {
            m_mainPanel = GetComponent<UIPanel>();
            m_titleContainer = m_mainPanel.Find<UIPanel>("TitleContainer");
            m_title = m_titleContainer.Find<UILabel>("Title");
            m_modIcon = m_titleContainer.Find<UISprite>("ModIcon");
            m_closeButton = m_titleContainer.Find<UIButton>("CloseButton");
            m_boxText = m_mainPanel.Find<UILabel>("BoxText");

            m_button1 = m_mainPanel.Find<UIButton>("ButtonAction1");
            m_button2 = m_mainPanel.Find<UIButton>("ButtonAction2");
            m_button3 = m_mainPanel.Find<UIButton>("ButtonAction3");
            m_button4 = m_mainPanel.Find<UIButton>("ButtonAction4");

            m_properties = m_mainPanel.GetComponent<BindPropertyByKey>();

            #region Events bindings
            m_mainPanel.enabled = false;

            m_button1.eventClicked += (x, y) => OnButton1();
            m_button2.eventClicked += (x, y) => OnButton2();
            m_button3.eventClicked += (x, y) => OnButton3();
            m_button4.eventClicked += (x, y) => OnButton4();


            KlyteMonoUtils.LimitWidthAndBox(m_title, out UIPanel boxContainerTitle);
            boxContainerTitle.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.Top;
            boxContainerTitle.relativePosition = new Vector3(0, 2);

            //This action allow centralize all calls to single object, coming from any mod
            m_mainPanel.objectUserData = new Action<Dictionary<string, object>, Action<int>>((Dictionary<string, object> properties, Action<int> callback) => Enqueue(BindProperties.FromDictionary(properties), callback));

            m_closeButton.eventClicked += (x, y) => Close(0);

            m_mainPanel.enabled = true;
            #endregion

        }

        private void OnButton1() => Close(1);

        private void OnButton2() => Close(2);

        private void OnButton3() => Close(3);

        private void OnButton4() => Close(4);

        private void Enqueue(BindProperties properties, Action<int> callback)
        {
            if (m_currentCallback == null)
            {
                UIView.library.ShowModal(PANEL_ID);
                m_currentCallback = callback;
                SetProperties(properties);
            }
            else
            {
                m_modalStack.Enqueue(Tuple.NewRef(ref properties, ref callback));
            }
        }

        private void Close(int result)
        {
            m_currentCallback?.Invoke(result);
            m_currentCallback = null;
            UIView.library.Hide(PANEL_ID);
            if (m_modalStack.Count > 0)
            {
                Tuple<BindProperties, Action<int>> next = m_modalStack.Dequeue();
                UIView.library.ShowModal(PANEL_ID);
                m_currentCallback = next.Second;
                SetProperties(next.First);
            }
        }

        private void SetProperties(BindProperties propertiesToSet)
        {
            m_properties.FindBinding("title").property.value = propertiesToSet.title;
            m_properties.FindBinding("icon").property.value = propertiesToSet.icon;
            m_properties.FindBinding("showClose").property.value = propertiesToSet.showClose;
            m_properties.FindBinding("message").property.value = propertiesToSet.message;
            m_properties.FindBinding("messageAlign").property.value = propertiesToSet.messageAlign;
            m_properties.FindBinding("showButton1").property.value = propertiesToSet.showButton1;
            m_properties.FindBinding("showButton2").property.value = propertiesToSet.showButton2;
            m_properties.FindBinding("showButton3").property.value = propertiesToSet.showButton3;
            m_properties.FindBinding("showButton4").property.value = propertiesToSet.showButton4;
            m_properties.FindBinding("textButton1").property.value = propertiesToSet.textButton1;
            m_properties.FindBinding("textButton2").property.value = propertiesToSet.textButton2;
            m_properties.FindBinding("textButton3").property.value = propertiesToSet.textButton3;
            m_properties.FindBinding("textButton4").property.value = propertiesToSet.textButton4;
        }

        private static Action<int> m_currentCallback;
        //queue to store the modal order
        private static readonly Queue<Tuple<BindProperties, Action<int>>> m_modalStack = new Queue<Tuple<BindProperties, Action<int>>>();

        private UIPanel m_mainPanel;
        private UIPanel m_titleContainer;
        private UILabel m_title;
        private UISprite m_modIcon;
        private UIButton m_closeButton;
        private UILabel m_boxText;
        private UIButton m_button1;
        private UIButton m_button2;
        private UIButton m_button3;
        private UIButton m_button4;
        private BindPropertyByKey m_properties;

        public static void ShowModal(BindProperties properties, Action<int> action)
        {
            UIComponent uIComponent = UIView.library.Get(PANEL_ID);
            if (uIComponent.objectUserData is Action<Dictionary<string, object>, Action<int>> addAction)
            {
                addAction(properties.ToDictionary(), action);
            }

        }

        internal struct BindProperties
        {
            public string title;
            public string icon;
            public bool showClose;
            public string message;
            public UIHorizontalAlignment messageAlign;
            public bool showButton1;
            public bool showButton2;
            public bool showButton3;
            public bool showButton4;
            public string textButton1;
            public string textButton2;
            public string textButton3;
            public string textButton4;

            public static BindProperties FromDictionary(Dictionary<string, object> dict)
            {
                var result = new BindProperties();
                foreach (KeyValuePair<string, object> kv in dict)
                {
                    switch (kv.Key)
                    {
                        case "title": result.title = (string)kv.Value; break;
                        case "icon": result.icon = (string)kv.Value; break;
                        case "showClose": result.showClose = (bool)kv.Value; break;
                        case "message": result.message = (string)kv.Value; break;
                        case "messageAlign": result.messageAlign = (UIHorizontalAlignment)kv.Value; break;
                        case "showButton1": result.showButton1 = (bool)kv.Value; break;
                        case "showButton2": result.showButton2 = (bool)kv.Value; break;
                        case "showButton3": result.showButton3 = (bool)kv.Value; break;
                        case "showButton4": result.showButton4 = (bool)kv.Value; break;
                        case "textButton1": result.textButton1 = (string)kv.Value; break;
                        case "textButton2": result.textButton2 = (string)kv.Value; break;
                        case "textButton3": result.textButton3 = (string)kv.Value; break;
                        case "textButton4": result.textButton4 = (string)kv.Value; break;
                    }
                }
                return result;
            }

            public Dictionary<string, object> ToDictionary()
            {
                return new Dictionary<string, object>()
                {
                    ["title"] = title,
                    ["icon"] = icon,
                    ["showClose"] = showClose,
                    ["message"] = message,
                    ["messageAlign"] = messageAlign,
                    ["showButton1"] = showButton1,
                    ["showButton2"] = showButton2,
                    ["showButton3"] = showButton3,
                    ["showButton4"] = showButton4,
                    ["textButton1"] = textButton1,
                    ["textButton2"] = textButton2,
                    ["textButton3"] = textButton3,
                    ["textButton4"] = textButton4
                };
            }



        }
    }
}