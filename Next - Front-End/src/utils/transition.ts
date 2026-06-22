export const safeViewTransition = (updateDOM: () => void) => {
  // Verifica se o navegador suporta a API
  if (document.startViewTransition) {
    document.startViewTransition(updateDOM);
  } else {
    // Se não suportar, atualiza o estado normalmente (sem animação)
    updateDOM();
  }
};