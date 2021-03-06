using UnityEngine;

namespace VBM {
    [System.Serializable]
    public class ListPropertyBinding : PropertyBinding {
        public Transform componentList;
        public ViewModelBinding componentElement;
        protected ListModel listModel;

        ~ListPropertyBinding() {
            if (listModel != null)
                UnbindList(listModel);
        }

        public override void OnPropertyChange(object value) {
            ElementCleared();
            if (listModel != null)
                UnbindList(listModel);

            listModel = value as ListModel;
            if (listModel != null) {
                InitChilds();
                BindList(listModel);
            }
        }

        protected virtual void BindList(object value) {
            listModel.elementAdded += ElementAdded;
            listModel.elementInserted += ElementInserted;
            listModel.elementSwaped += ElementSwaped;
            listModel.elementRemoved += ElementRemoved;
            listModel.elementRemovRanged += ElementRemovRanged;
            listModel.elementCleared += ElementCleared;
        }

        protected virtual void UnbindList(object value) {
            listModel.elementAdded -= ElementAdded;
            listModel.elementInserted -= ElementInserted;
            listModel.elementSwaped -= ElementSwaped;
            listModel.elementRemoved -= ElementRemoved;
            listModel.elementRemovRanged += ElementRemovRanged;
            listModel.elementCleared -= ElementCleared;
        }

        protected void InitChilds() {
            for (int i = 0; i < listModel.Count; i++) {
                CreateChild(listModel[i], i);
            }
        }

        protected Transform CreateChild(IModel model, int index) {
            Transform child;
            if (index < componentList.childCount) {
                child = componentList.GetChild(index);
            } else {
                child = Object.Instantiate(componentElement.transform, Vector3.zero, Quaternion.identity, componentList);
            }
            child.gameObject.SetActive(true);
            ViewModelBinding binding = child.GetComponent<ViewModelBinding>();
            binding.SetModel(model);
            return child;
        }

        protected void ElementAdded(IModel model) {
            CreateChild(model, listModel.Count - 1);
        }

        protected void ElementInserted(int index, IModel model) {
            Transform child = CreateChild(model, listModel.Count - 1);
            child.SetSiblingIndex(index);
        }

        protected void ElementSwaped(int index1, int index2) {
            Transform child1 = componentList.GetChild(index1);
            Transform child2 = componentList.GetChild(index2);
            child2.SetSiblingIndex(index1);
            child1.SetSiblingIndex(index2);
        }

        protected void ElementRemoved(int index) {
            Transform child = componentList.GetChild(index);
            child.SetAsLastSibling();
            child.gameObject.SetActive(false);
        }

        protected void ElementRemovRanged(int index, int count) {
            for (int i = count - 1; i >= 0; i--) {
                ElementRemoved(index + i);
            }
        }

        protected void ElementCleared() {
            foreach (Transform transform in componentList) {
                transform.gameObject.SetActive(false);
            }
        }
    }
}