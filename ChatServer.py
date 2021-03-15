'''
Charles Farris
COMP 332, Fall 2018
Chat server

Usage:
    python3 chat_server.py <host> <port>
'''

# Import classes
import socket
import sys
import threading

class ChatServer():

    # Initilization function
    def __init__(self, server_host, server_port):
        self.server_host = server_host
        self.server_port = server_port
        self.server_backlog = 1
        self.chat_list = {}
        self.chat_id = 0
        self.lock = threading.Lock()
        self.start()

    def start(self):

        # Initialize server socket on which to listen for connections
        try:
            server_sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            server_sock.bind((self.server_host, self.server_port))
            server_sock.listen(self.server_backlog)
            print ("Server Ready")
        except OSError as e:
            print ("Unable to open server socket")
            if server_sock:
                server_sock.close()
            sys.exit(1)

        # Wait for user connection
        while True:
            conn, addr = server_sock.accept()
            self.add_user(conn, addr)
            thread = threading.Thread(target = self.serve_user,
                    args = (conn, addr, self.chat_id))
            thread.start()

    def add_user(self, conn, addr):
        print ('User has connected', addr)
        self.chat_id = self.chat_id + 1
        self.lock.acquire()
        self.chat_list[self.chat_id] = (conn, addr)
        self.lock.release()

    def read_data(self, conn, bin_data):
        '''
        Format of data: "length.user: data"
        '''

        while True:

            # Read from socket
            more = conn.recv(1024)
            if more == b'':
                return ['', '', 1]
            bin_data += more

            try:
                # Get msg length
                str_data = bin_data.decode('utf-8')
                idx = str_data.index('.')
                slen = int(str_data[ : idx])

                # Check whether full msg received
                if slen <= len(str_data[idx : ]):
                    msg = str_data[ : idx + slen + 1]
                    bin_data = bin_data[idx + slen + 1:]
                    return [msg, bin_data, 0]

            except ValueError as e:
                pass

    def send_data(self, user, data):
        self.lock.acquire()
        bin_data = data.encode('utf-8')

        # Send msg to all users
        for i in self.chat_list:
            if i != user:
                entry = self.chat_list[i]
                conn = entry[0]
                conn.sendall(bin_data)

        self.lock.release()

    def cleanup(self, conn, user):
        self.lock.acquire()
        del self.chat_list[user]
        self.lock.release()

    def serve_user(self, conn, addr, user):
        bin_data = b''
        while True:
            [msg, bin_data, flag] = self.read_data(conn, bin_data)
            if flag == 1:
                break
            self.send_data(user, msg)
        self.cleanup(conn, user)

def main():

    server_host = 'localhost'
    server_port = 50007
    if len(sys.argv) > 1:
        server_host = sys.argv[1]
        server_port = int(sys.argv[2])

    chat_server = ChatServer(server_host, server_port)

if __name__ == '__main__':
    main()
