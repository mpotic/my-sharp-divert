# WinDivert Message Interceptor and Modifier

**Introduction:**

This repository represents the culmination of a Bachelor's project. It aims to demonstrate the security performance of a communication system. In this specific project, a C# wrapper for WinDivert version 2.2 has been developed, enabling the interception and modification of messages exchanged between proxies.

## Project Overview:

The primary objective of this project is to evaluate the security measures implemented in the communication system, with a particular focus on the signing and encryption mechanisms employed by the proxies. By intercepting and modifying messages, it can be assessed how effectively these security features safeguard the integrity and confidentiality of transmitted data.

- **Interception**: The application is capable of intercepting TCP messages between proxies within the communication system, designed to capture messages as they traverse the network.

- **Message Reading**: Intercepted messages undergo reading and analysis, allowing for an inspection of their content and structure.

- **Message Modification**: In addition to reading messages, the application possesses the ability to modify message contents. This functionality plays a pivotal role in security performance testing, facilitating the simulation of various scenarios and the assessment of the communication system's resilience.

## Usage:

To utilize this application, users should ensure the proper installation of WinDivert version 2.2 on their system. The official WinDivert website (https://reqrypt.org/windivert.html) provides the necessary resources for download and installation. Please note that earlier or later versions may not function as expected.

## Repository structure:

This repository houses the source code for the WinDivert C# wrapper application and the packet sniffer application that utilises the wrapper and intercepts and modifies the traffic of related repositories proxies.

## Related Repository:

For a full context for this bachelor's project, including the master application that engages with a slave simulator using proxies for security purposes (signing, encryption), please refer to the related repository: https://github.com/mpotic/modbus-scada.

## Disclaimer:

This project serves exclusively for academic and research purposes. Users are cautioned against employing this application for unauthorized or malicious activities. Compliance with applicable laws and regulations governing network monitoring and data interception is imperative.

Feel free to explore the code and experiment with the intercept and modify capabilities to gain a deeper understanding of the security performance of the communication system.

**Author:** Miloš Potić **Date:** 15.09.2023.