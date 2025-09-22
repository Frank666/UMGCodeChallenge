import { render, screen, waitFor } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import { vi } from "vitest";
import Main from "./main";
import '@testing-library/jest-dom';


describe("Main Component", () => {
  beforeEach(() => {
    vi.restoreAllMocks();
  });

  it("renders all form inputs and button", () => {
    render(<Main />);
    expect(screen.getByTestId("musicContracts")).toBeInTheDocument();
    expect(screen.getByTestId("partnerContracts")).toBeInTheDocument();
    expect(screen.getByTestId("partner")).toBeInTheDocument();
    expect(screen.getByTestId("date")).toBeInTheDocument();
  });

  it("shows error if fields are empty on submit", async () => {
    render(<Main />);
    userEvent.click(screen.getByRole("button", { name: /Buscar contratos/i }));
    expect(await screen.findByText(/Todos los campos son requeridos/i)).toBeInTheDocument();
  });
});
