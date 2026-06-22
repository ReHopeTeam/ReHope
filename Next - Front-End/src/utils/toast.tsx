import Button from "@/components/button/button";
import { Slide, toast, ToastOptions } from "react-toastify";

const defaultOptions: ToastOptions = {
  position: "bottom-right",
  autoClose: 2000,
  closeOnClick: true,
  draggable: true,
  transition: Slide,
};

export const notificacao = (msg: string) => toast.success(msg, defaultOptions);
export const erro = (msg: string) => toast.error(msg, defaultOptions);

export const toastConfirmarExcluir = (aoConfirmar: () => void) => {
  toast(
    ({ closeToast }) => (
      <div className="column">
        <p>Deseja realmente excluir?</p>
        <div className="sbs">
          <Button
            className="btn2 small_height"
            onClick={closeToast}
            children="Cancelar"
          />
          <Button
            className="btn small_height"
            onClick={() => {
              aoConfirmar();
              closeToast();
            }}
            children="Sim"
          />
        </div>
      </div>
    ),
    {
      autoClose: false,
      closeOnClick: false,
      draggable: false,
      transition: Slide,
    },
  );
};
