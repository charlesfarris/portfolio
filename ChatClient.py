'''
Charles Farris
COMP 332, Fall 2018
Chat client

Example usage:
    python3 chat_client.py <chat_host> <chat_port>

'''

# Import classes
import socket
import sys
import threading


class ChatClient:

    # Initialization function
    def __init__(self, chat_host, chat_port, user_name):
        self.chat_host = chat_host
        self.chat_port = chat_port
        self.user_name = user_name
        self.start()

    def start(self):

        # Open connection to chat
        try:
            chat_sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            chat_sock.connect((self.chat_host, self.chat_port))
            print("Connected to socket")
        except OSError as e:
            print("Unable to connect to socket: ")
            if chat_sock:
                chat_sock.close()
            sys.exit(1)

        threading.Thread(target=self.write_sock, args=(chat_sock,)).start()
        threading.Thread(target=self.read_sock, args=(chat_sock,)).start()

    def write_sock(self, sock):
        '''
        Format of data: "length.user: data"
        '''

        while True:
            data = input('')
            msg = self.user_name + ': ' + data
            bin_msg = (str(len(msg)) + '.' + msg).encode('utf-8')
            sock.sendall(bin_msg)

    def read_sock(self, sock):
        '''
        Format of data: "length.user: data"
        '''
        bin_reply = b''

        while True:
            # Read from socket
            more = sock.recv(1024)
            if more == b'':
                return
            bin_reply += more

            # Check whether full msg received
            try:
                str_reply = bin_reply.decode('utf-8')
                idx = str_reply.index('.')
                slen = int(str_reply[ : idx])
                if slen <= len(str_reply[idx : ]):
                    msg = str_reply[ idx + 1 : idx + slen + 1]
                    bin_reply = bin_reply[idx + slen + 1:]
                    print(msg)
            except ValueError as e:
                pass

def main():

    print (sys.argv, len(sys.argv))
    chat_host = 'localhost'
    chat_port = 50007
    user_name = 'Charlie'

    if len(sys.argv) > 1:
        chat_host = sys.argv[1]
        chat_port = int(sys.argv[2])
        user_name = sys.argv[3]

    try:
        chat_client = ChatClient(chat_host, chat_port, user_name)
    except KeyboardInterrupt:
        pass


if __name__ == '__main__':
    main()
