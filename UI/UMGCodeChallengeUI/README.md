# Global Rights Management (GRM) Platform – UMGCodeChallenge

## Overview

This project implements a **Global Rights Management (GRM) platform** code challenge 


The solution demonstrates **Clean Architecture principles**, **best practices in C# and React**, and includes **unit tests** for both backend and frontend components.

---

## Project Structure

- **`UI/`** – Contains the frontend implemented with **React and Vite**.  
  - The UI allows users to upload **Music Contracts** and **Partner Contracts** files, select a distribution partner, and an effective date.  
  - It displays the active contracts in a table.  

- **`UMGCodeChallenge/`** – Contains the backend implemented in **.NET 8 Minimal API**.  
  - Provides the `/contracts/upload` endpoint to process uploaded files and return active contracts.  
  - Includes **two sample TXT files** for testing (`MusicContracts.txt` and `PartnerContracts.txt`).  
  - Implements **Clean Architecture principles** and **SOLID** design.  
  - Unit tests are included for both file parsing and business logic.

---

## Getting Started

### Prerequisites

- Node.js >= 18  
- .NET 8 SDK  

### Running the application

From the **root of the frontend (`UI/`)** folder, run:

```bash
npm install
npm run start:all
