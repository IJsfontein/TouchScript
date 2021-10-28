﻿#if TOUCHSCRIPT_DEBUG

using System;
using System.Collections.Generic;
using System.Text;
using TouchScript.Debugging.Filters;
using TouchScript.Debugging.Loggers;
using TouchScript.Pointers;
using TouchScript.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace TouchScript.Debugging
{
    public class TextPointerLogger : MonoBehaviour, IPointerLogger
    {
        [SerializeField] private Text text;
        [SerializeField] private int numLines = 10;
        
        /// <inheritdoc />
        public int PointerCount
        {
            get { throw new NotImplementedException("TextPointerLogger doesn't support reading data."); }
        }

        private List<string> lines = new List<string>();
        private StringBuilder linesBuilder = new StringBuilder();

        private void Awake()
        {
            TouchScriptDebugger.Instance.PointerLogger = this;
        }

        private void LateUpdate()
        {
            linesBuilder.Clear();
            
            foreach (var line in lines)
            {
                linesBuilder.AppendLine(line);
            }

            text.text = linesBuilder.ToString();
        }
        
        public void Log(Pointer pointer, PointerEvent evt)
        {
            var path = TransformUtils.GetHeirarchyPath(pointer.GetPressData().Target);

            var line =
                $"{pointer.Type}({pointer.Id}):({pointer.Position.x}, {pointer.Position.y}) | ({pointer.PreviousPosition.x}, {pointer.PreviousPosition.y}) - ({path ?? ""})";

            lines.Add(line);
            if (lines.Count > numLines)
            {
                lines.RemoveAt(0);
            }
        }

        public List<PointerData> GetFilteredPointerData(IPointerDataFilter filter = null)
        {
            throw new NotImplementedException("TextPointerLogger doesn't support reading data.");
        }

        public List<PointerLog> GetFilteredLogsForPointer(int id, IPointerLogFilter filter = null)
        {
            throw new NotImplementedException("TextPointerLogger doesn't support reading data.");
        }

        public void Dispose() { }
    }
}

#endif