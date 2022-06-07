import { AuthClient } from "@dfinity/auth-client";
import { HttpAgent } from "@dfinity/agent";

window.HttpAgentType = HttpAgent;
window.authClient = await AuthClient.create();