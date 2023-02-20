interface FegyelmiUgyMuvelet {
  Id: Number;
  Text: String;
  ColorClass: String;
  ModalIdToOpen: String;
  FunctionToRun?: String;
  ModalType?: Number;
  Options?: Object;
  Sorrend?: Number;
}
