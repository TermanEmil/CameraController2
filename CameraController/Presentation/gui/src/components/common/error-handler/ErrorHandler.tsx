import ErrorSnackbarManager from 'components/common/error-snackbar-manager/ErrorSnackbarManager';
import React, { createContext, useContext, useState } from 'react';

type ErrorHandlingAction = (errorMessage: string) => void;
const ErrorHandlingContext = createContext<ErrorHandlingAction | null>(null);

interface ErrorHandlerProps {
  children: React.ReactNode;
}

export function ErrorHandler({ children }: ErrorHandlerProps): JSX.Element {
  const [errorMessage, setErrorMessage] = useState('');
  const [errorId, setErrorId] = useState(0);

  function handleError(error: string) {
    setErrorMessage(error);
    setErrorId(errorId + 1);
  }

  return (
    <ErrorHandlingContext.Provider value={handleError}>
      <ErrorSnackbarManager errorMessage={errorMessage} errorId={errorId}></ErrorSnackbarManager>
      {children}
    </ErrorHandlingContext.Provider>
  );
}

export function useErrorHandler(): ErrorHandlingAction {
  const context = useContext(ErrorHandlingContext);

  if (!context) {
    throw new Error('useErrorHandler must be used within ErrorHandlingContext');
  }

  return context;
}
