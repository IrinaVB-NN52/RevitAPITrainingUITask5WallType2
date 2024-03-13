using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingUITask5WallType2
{
    internal class MainViewViewModel
    {
        private ExternalCommandData _commandData;

        public DelegateCommand SaveCommand { get; }
        public List<Element> PickedObjects { get; } = new List<Element>();
        public List<WallType> WallTypes { get; } = new List<WallType>();


        public WallType SelectedWallType { get; set; }


        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            WallTypes = WallsUtils.GetWallTypes(commandData);
            SaveCommand = new DelegateCommand(OnSaveCommand);
            PickedObjects = SelectionUtils.PickObjects(commandData);


        }

        private void OnSaveCommand()
        {
            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            if (PickedObjects.Count == 0 || SelectedWallType == null)
                return;

            using (var ts = new Transaction(doc, "Изминение типа стен"))
            {
                ts.Start();

                foreach (var pickedObject in PickedObjects)
                {
                    if (pickedObject is WallType)
                    {
                        var Wall = pickedObject as WallType;

                    }
                }

                ts.Commit();
            }

            RaiseCloseRequest();
        }

        public event EventHandler CloseRequest;
        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }

    }
}
